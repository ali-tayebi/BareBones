using System.Threading;
using System.Threading.Tasks;
using BareBones.Core;
using Microsoft.EntityFrameworkCore;

namespace BareBones.Persistence.EntityFramework.Migration
{
    public class DataSeedingStartupTask<TDbContext> : IStartupTask
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly IDataSeeder _dataSeeder;

        public DataSeedingStartupTask(
            TDbContext dbContext,
            IDataSeeder dataSeeder)
        {
            _dbContext = dbContext;
            _dataSeeder = dataSeeder;
        }
        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            await _dataSeeder.SeedAsync(_dbContext);
        }
    }
}
