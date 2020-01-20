using BareBones.CQRS.Queries;

namespace Ordering.API.Application.Features.GettingOrdersById
{
    public class GetOrderByIdQuery : IQuery<GetOrderByIdQueryResult>
    {
        public int OrderId { get; set; }
    }
}
