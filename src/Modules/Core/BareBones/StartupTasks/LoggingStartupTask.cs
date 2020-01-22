using System.Threading;
using System.Threading.Tasks;
using BareBones;
using Microsoft.Extensions.Logging;

namespace BareBones.StartupTasks
{
    public class LoggingStartupTask : IStartupTask
    {
        private readonly ILogger<LoggingStartupTask> _logger;

        public LoggingStartupTask(ILogger<LoggingStartupTask> logger)
        {
            _logger = logger;
        }
        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Service is starting...");
            return Task.CompletedTask;
        }
    }
}
