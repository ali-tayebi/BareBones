using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BareBones.CQRS.Commands.Dispatchers.Filters
{
    public class LoggingCommandGatewayFilter : ICommandGatewayFilter
    {
        private readonly ILogger<LoggingCommandGatewayFilter> _logger;

        public LoggingCommandGatewayFilter(ILogger<LoggingCommandGatewayFilter> logger)
        {
            _logger = logger;
        }
        public Task OnHandlingAsync<TCommand>(CommandHandlingContext<TCommand> context) where TCommand : class, ICommand
        {
            _logger.LogInformation("----- Handling command {CommandName} ({@Command})", context.Command.GetType().FullName, context.Command);
            return Task.CompletedTask;
        }

        public Task OnHandledAsync<TCommand, TResult>(CommandHandledContext<TCommand, TResult> context) where TCommand : class, ICommand
        {
            _logger.LogInformation("----- Command {CommandName} handled - result: {@Result}", context.Command.GetType().FullName, context.Result);
            return Task.CompletedTask;
        }
    }
}
