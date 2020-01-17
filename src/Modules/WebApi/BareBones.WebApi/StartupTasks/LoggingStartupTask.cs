using System.Threading;
using System.Threading.Tasks;
using BareBones.Core;
using Microsoft.Extensions.Logging;

namespace BareBones.WebApi
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
