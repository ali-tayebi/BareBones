using System.Threading;
using System.Threading.Tasks;

namespace BareBones.CQRS.CommandGateway
{
    public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand
    {
        Task<TResult> HandleAsync(TCommand query, CancellationToken cancellationToken = default);
    }
}
