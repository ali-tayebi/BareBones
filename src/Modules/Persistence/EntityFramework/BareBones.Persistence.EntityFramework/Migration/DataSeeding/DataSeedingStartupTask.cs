using System;
using System.Threading;
using System.Threading.Tasks;
using BareBones;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BareBones.Persistence.EntityFramework.Migration
{
    public class DataSeedingStartupTask<TDbContext> : IStartupTask
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;

        public DataSeedingStartupTask(
            TDbContext dbContext,
            IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }
        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var dataSeeder = _serviceProvider.GetService<IDbDataSeeder<TDbContext>>();
            if (dataSeeder != null)
            {
                await dataSeeder.SeedAsync(_dbContext);
            }
        }
    }
}
