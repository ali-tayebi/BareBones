using System.Threading.Tasks;
using BareBones.Persistence.EntityFramework;
using Ordering.Domain;

namespace Ordering.Persistence.Repositories
{
    public class OrderRepositoryBase : RepositoryBase<Order, int>, IOrderRepository
    {
        public OrderRepositoryBase(
            OrderingDbContext dbContext,
            IExecutionContext currentRequestState)
            : base(dbContext, currentRequestState)
        {
        }

        public new Task<Order> GetByIdAsync(int orderId)
        {
            return base.GetByIdAsync(orderId);
        }
    }
}
