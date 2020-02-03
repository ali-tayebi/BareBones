using System.Threading;
using System.Threading.Tasks;

namespace BareBones.CQRS.Commands.Dispatchers
{

    public interface ICommandDispatcher<in TCommand, TResult> where TCommand : class, ICommand
    {
        Task<TResult> SendAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
