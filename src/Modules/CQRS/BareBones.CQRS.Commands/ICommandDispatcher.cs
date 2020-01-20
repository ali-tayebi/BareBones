using System.Threading;
using System.Threading.Tasks;

namespace BareBones.CQRS.Commands
{
    public interface ICommandDispatcher
    {
        Task<TResult> SendAsync<TResult>(
            ICommand<TResult> command,
            CancellationToken  cancellationToken = default);
    }
}
