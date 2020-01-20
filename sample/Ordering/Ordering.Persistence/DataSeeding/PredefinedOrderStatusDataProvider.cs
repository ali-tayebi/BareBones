using System.Collections.Generic;
using BareBones.Persistence.EntityFramework.Migration;
using Ordering.Domain;

namespace Ordering.Persistence.DataSeeding
{
    public class PredefinedOrderStatusDataProvider :
        IDbDataProvider<IEnumerable<OrderStatus>>
    {
        public IEnumerable<OrderStatus> GetData()
        {
            return new []
            {
                OrderStatus.Submitted,
                OrderStatus.AwaitingValidation,
                OrderStatus.StockConfirmed,
                OrderStatus.Paid,
                OrderStatus.Shipped,
                OrderStatus.Cancelled
            };
        }
    }
}
