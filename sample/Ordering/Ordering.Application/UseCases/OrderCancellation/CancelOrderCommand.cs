using System.Runtime.Serialization;
using BareBones.CQRS;

namespace Ordering.Application.UseCases.OrderCancellation
{
    public class CancelOrderCommand : ICommand<CancelOrderCommandResult>
    {
        public string CommandUniqueIdentity => OrderId.ToString();

        [DataMember]
        public int OrderId { get; }

        public CancelOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
