using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BareBones.CQRS.Commands
{
    public class MediatorCommandDispatcher : ICommandDispatcher
    {
        private readonly IMediator _mediator;

        public MediatorCommandDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<TResult> SendAsync<TResult>(
            ICommand<TResult> command,
            CancellationToken  cancellationToken = default)
        {
            return _mediator.Send(command, cancellationToken);
        }
    }
}
