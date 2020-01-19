using System.Threading.Tasks;
using BareBones.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ordering.Infrastructure
{
    public class OrderingDbContextDesignFactory : IDesignTimeDbContextFactory<OrderingDbContext>
    {
        public OrderingDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderingDbContext>()
                .UseSqlServer("Server=tcp:127.0.0.1,1433;Database=OrderingDb;User Id=sa;Password=Pass@word;");

            return new OrderingDbContext(
                optionsBuilder.Options,
                new []{ new EmptyDbContextInterceptor()});
        }

        class EmptyDbContextInterceptor : IDbContextInterceptor
        {
            public Task BeforeSaveChangesAsync(DbContextBase dbContextBase)
            {
                return Task.CompletedTask;
            }

            public Task AfterSaveChangesAsync(DbContextBase dbContextBase)
            {
                return Task.CompletedTask;
            }
        }
    }
}
