using BareBones.Domain.Aggregates;

namespace Ordering.Domain.Events
{
    public class OrderCancelledDomainEvent : IDomainEvent
    {
        public Order Order { get; }

        public OrderCancelledDomainEvent(Order order)
        {
            Order = order;
        }
    }
}
