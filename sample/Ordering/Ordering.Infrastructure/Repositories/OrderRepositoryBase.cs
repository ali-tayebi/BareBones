using System.Threading.Tasks;
using BareBones.Persistence.EntityFramework;
using Ordering.Domain;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepositoryBase : RepositoryBase<Order, int>, IOrderRepository
    {
        public OrderRepositoryBase(
            OrderingDbContextBase dbContextBase,
            IExecutionContext currentRequestState)
            : base(dbContextBase, currentRequestState)
        {
        }

        public new Task<Order> GetByIdAsync(int orderId)
        {
            return base.GetByIdAsync(orderId);
        }
    }
}
