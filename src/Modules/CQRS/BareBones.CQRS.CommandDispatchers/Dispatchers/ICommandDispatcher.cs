using System.Threading;
using System.Threading.Tasks;

namespace BareBones.CQRS.CommandDispatchers.Dispatchers
{

    public interface ICommandDispatcher
    {
        Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand;
    }
}
