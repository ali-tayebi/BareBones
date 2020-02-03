using System.Threading.Tasks;
using BareBones.CQRS;
using BareBones.CQRS.Commands;

namespace BareBones.CQRS.CommandDispatchers.Filters
{
    public interface ICommandDispatchFilter
    {
        Task OnDispatchingAsync<TCommand>(CommandDispatchingContext<TCommand> context) where TCommand : class, ICommand;
        Task OnDispatchedAsync<TCommand, TResult>(CommandDispatchedContext<TCommand, TResult> context)  where TCommand : class, ICommand;
    }
}
