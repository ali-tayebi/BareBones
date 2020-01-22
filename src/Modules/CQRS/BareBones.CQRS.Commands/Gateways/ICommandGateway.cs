using System.Threading;
using System.Threading.Tasks;

namespace BareBones.CQRS.Commands.Gateways
{
    public interface ICommandGateway
    {
        Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand;
    }
}
