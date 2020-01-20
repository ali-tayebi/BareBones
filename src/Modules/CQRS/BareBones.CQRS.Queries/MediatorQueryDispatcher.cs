using System.Threading.Tasks;
using MediatR;

namespace BareBones.CQRS.Queries
{
    public class MediatorQueryDispatcher : IQueryDispatcher
    {
        private readonly IMediator _mediator;

        public MediatorQueryDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<TResult> SendAsync<TResult>(IQuery<TResult> query)
        {
            return _mediator.Send(query);
        }
    }
}
