using BareBones.CQRS.Queries;

namespace Ordering.API.Application.Features.GettingOrdersById
{
    public class GetOrderByIdQuery : IQuery
    {
        public int OrderId { get; set; }
    }
}
