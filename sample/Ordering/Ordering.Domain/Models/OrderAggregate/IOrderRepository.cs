using System.Threading.Tasks;
using BareBones.Domain.Repository;

namespace Ordering.Domain
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetByIdAsync(int orderId);
    }
}
