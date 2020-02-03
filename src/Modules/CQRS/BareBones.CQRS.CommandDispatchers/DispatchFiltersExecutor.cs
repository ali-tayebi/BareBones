using System.Collections.Generic;
using System.Threading.Tasks;
using BareBones.CQRS.CommandDispatchers.Filters;

namespace BareBones.CQRS.CommandDispatchers
{
    public class DispatchFiltersExecutor : IDispatchFiltersExecutor
    {
        private readonly IEnumerable<ICommandDispatchFilter> _filters;

        public DispatchFiltersExecutor(IEnumerable<ICommandDispatchFilter> filters)
        {
            _filters = filters;
        }

        public async Task ExecuteFiltersBeforeDispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            foreach (var filter in _filters)
            {
                await filter.OnDispatchingAsync(new CommandDispatchingContext<TCommand>(command));
            }
        }

        public async Task ExecuteFiltersAfterDispatchAsync<TCommand, TResult>(TCommand command, TResult result) where TCommand : class, ICommand
        {
            foreach (var filter in _filters)
            {
                await filter.OnDispatchedAsync(new CommandDispatchedContext<TCommand, TResult>(command, result));
            }
        }
    }
}
