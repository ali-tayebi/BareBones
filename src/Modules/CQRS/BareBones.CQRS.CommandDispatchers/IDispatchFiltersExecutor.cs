using System.Threading.Tasks;
using BareBones.CQRS;

namespace BareBones.CQRS.CommandDispatchers
{
    public interface IDispatchFiltersExecutor
    {
        Task ExecuteFiltersBeforeDispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
        Task ExecuteFiltersAfterDispatchAsync<TCommand, TResult>(TCommand command, TResult result) where TCommand : class, ICommand;
    }
}
