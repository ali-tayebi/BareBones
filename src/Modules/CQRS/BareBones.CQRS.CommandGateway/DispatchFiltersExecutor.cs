using System.Collections.Generic;
using System.Threading.Tasks;
using BareBones.CQRS.Commands.Dispatchers.Filters;

namespace BareBones.CQRS.Commands.Dispatchers
{
    public class CommandGatewayFiltersExecutor : ICommandGatewayFiltersExecutor
    {
        private readonly IEnumerable<ICommandGatewayFilter> _filters;

        public CommandGatewayFiltersExecutor(IEnumerable<ICommandGatewayFilter> filters)
        {
            _filters = filters;
        }

        public async Task ExecuteFiltersBeforeHandlerAsync<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            foreach (var filter in _filters)
            {
                await filter.OnHandlingAsync(new CommandHandlingContext<TCommand>(command));
            }
        }

        public async Task ExecuteFiltersAfterHandlerAsync<TCommand, TResult>(TCommand command, TResult result) where TCommand : class, ICommand
        {
            foreach (var filter in _filters)
            {
                await filter.OnHandledAsync(new CommandHandledContext<TCommand, TResult>(command, result));
            }
        }
    }
}
