using System.Runtime.Serialization;
using BareBones.CQRS;
using Ordering.API.Application.Features.OrderCancellation;

namespace Ordering.Application.UseCases.OrderCancellation
{
    public class CancelOrderCommand : ICommand
    {
        public string IdentityKey => OrderId.ToString();

        [DataMember]
        public int OrderId { get; }

        public CancelOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
