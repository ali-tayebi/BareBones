using System.Threading.Tasks;
using BareBones.CQRS;
using BareBones.CQRS.Commands;
using Microsoft.Extensions.Logging;

namespace BareBones.CQRS.CommandDispatchers.Filters
{
    public class LoggingCommandDispatchFilter : ICommandDispatchFilter
    {
        private readonly ILogger<LoggingCommandDispatchFilter> _logger;

        public LoggingCommandDispatchFilter(ILogger<LoggingCommandDispatchFilter> logger)
        {
            _logger = logger;
        }

        public Task OnDispatchingAsync<TCommand>(CommandDispatchingContext<TCommand> context) where TCommand : class, ICommand
        {
            _logger.LogInformation("----- Dispatching command {CommandName} ({@Command})", context.Command.GetType().FullName, context.Command);
            return Task.CompletedTask;
        }


        public Task OnDispatchedAsync<TCommand, TResult>(CommandDispatchedContext<TCommand, TResult> context) where TCommand : class, ICommand
        {
            _logger.LogInformation("----- Command {CommandName} dispatched - result: {@Result}", context.Command.GetType().FullName, context.Result);
            return Task.CompletedTask;
        }
    }
}
