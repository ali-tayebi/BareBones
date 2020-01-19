using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BareBones.Persistence.EntityFramework.Migration
{
    public interface IDbDataSeeder<TDbContext> where TDbContext : DbContext
    {
        Task SeedAsync(TDbContext dbContext);
    }
}
