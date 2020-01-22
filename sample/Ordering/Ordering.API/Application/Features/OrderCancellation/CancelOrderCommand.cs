using System.Runtime.Serialization;
using BareBones.CQRS.Commands;
using Ordering.API.Application.Features.OrderCancellation;

namespace Ordering.Application.UseCases.OrderCancellation
{
    public class CancelOrderCommand : ICommand
    {
        public string UniqueId => OrderId.ToString();

        [DataMember]
        public int OrderId { get; }

        public CancelOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
