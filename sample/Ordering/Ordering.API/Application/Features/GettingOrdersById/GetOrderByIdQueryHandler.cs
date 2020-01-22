using System.Threading;
using System.Threading.Tasks;
using BareBones.CQRS.Queries;

namespace Ordering.API.Application.Features.GettingOrdersById
{
    public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, GetOrderByIdQueryResult>
    {
        public Task<GetOrderByIdQueryResult> HandleAsync(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetOrderByIdQueryResult
            {
                OrderNumber = request.OrderId
            });
        }
    }
}
