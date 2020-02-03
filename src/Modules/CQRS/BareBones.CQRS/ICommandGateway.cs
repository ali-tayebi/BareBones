using System;
using System.Threading;
using System.Threading.Tasks;

namespace BareBones.CQRS
{
    public interface ICommandGateway
    {
        Task<TResult> HandleAsync<TCommand, TResult>(TCommand query, CancellationToken cancellationToken = default) where TCommand : class, ICommand;
    }
}
