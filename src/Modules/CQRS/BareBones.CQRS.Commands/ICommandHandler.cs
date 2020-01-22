using System.Threading;
using System.Threading.Tasks;

namespace BareBones.CQRS.Commands
{
    public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand
    {
        Task<TResult> HandleAsync(TCommand request, CancellationToken cancellationToken = default);
    }
}
