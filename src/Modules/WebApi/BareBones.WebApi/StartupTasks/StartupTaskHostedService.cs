using System;
using System.Threading;
using System.Threading.Tasks;
using BareBones.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BareBones.WebApi
{
    public class StartupTaskHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public StartupTaskHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<StartupTaskHostedService>>();
                var startupTasks = scope.ServiceProvider.GetServices<IStartupTask>();

                foreach (var task in startupTasks)
                {
                    try
                    {
                        await task.ExecuteAsync(cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex.HResult, ex, ex.Message);
                        throw new StartupTaskFatalException(task.GetType(), ex);
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }

    [Serializable]
    public class StartupTaskFatalException : Exception
    {
        public StartupTaskFatalException(Type startupTask, Exception innerException)
            : base($"Fatal error in running startup task ({startupTask.FullName})", innerException)
        {
            StartupTask = startupTask;
        }
        public Type StartupTask { get; }
    }
}
