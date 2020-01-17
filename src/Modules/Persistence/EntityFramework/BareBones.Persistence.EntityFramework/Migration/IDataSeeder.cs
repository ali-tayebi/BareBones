using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BareBones.Persistence.EntityFramework.Migration
{
    public interface IDataSeeder
    {
        Task SeedAsync<TDbContext>(TDbContext dbContext) where TDbContext : DbContext;
    }
}
