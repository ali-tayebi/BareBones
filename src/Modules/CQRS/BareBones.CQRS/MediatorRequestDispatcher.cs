using System.Threading.Tasks;
using MediatR;

namespace BareBones.CQRS
{
    public class MediatorRequestDispatcher : IRequestDispatcher
    {
        private readonly IMediator _mediator;

        public MediatorRequestDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<TResult> Dispatch<TResult>(ICommand<TResult> command)
        {
            return _mediator.Send(command);
        }

        public Task<TResult> Dispatch<TResult>(IQuery<TResult> query)
        {
            return _mediator.Send(query);
        }
    }
}
