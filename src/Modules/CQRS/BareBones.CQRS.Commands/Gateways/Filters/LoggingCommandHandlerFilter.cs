using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BareBones.CQRS.Commands.Gateways.Filters
{
    public class LoggingCommandHandlerFilter : ICommandHandlerFilter
    {
        private readonly ILogger<LoggingCommandHandlerFilter> _logger;

        public LoggingCommandHandlerFilter(ILogger<LoggingCommandHandlerFilter> logger)
        {
            _logger = logger;
        }
        public Task OnHandlerExecutingAsync<TCommand>(CommandHandlerExecutingContext<TCommand> context)
        {
            _logger.LogInformation("----- Handling command {CommandName} ({@Command})", context.Command.GetType().FullName, context.Command);
            return Task.CompletedTask;
        }

        public Task OnHandlerExecutedAsync<TCommand, TResult>(CommandHandlerExecutedContext<TCommand, TResult> context)
        {
            _logger.LogInformation("----- Command {CommandName} handled - result: {@Result}", context.Command.GetType().FullName, context.Result);
            return Task.CompletedTask;
        }
    }
}
