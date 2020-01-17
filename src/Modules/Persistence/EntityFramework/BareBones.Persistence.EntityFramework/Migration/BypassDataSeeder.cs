using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BareBones.Persistence.EntityFramework.Migration
{
    public class BypassDataSeeder : IDataSeeder
    {
        private readonly ILogger<BypassDataSeeder> _logger;

        public BypassDataSeeder(ILogger<BypassDataSeeder> logger)
        {
            _logger = logger;
        }

        public Task SeedAsync<TDbContext>(TDbContext dbContext) where TDbContext : DbContext
        {
            _logger.LogInformation("Data seeding is bypassed");
            return Task.CompletedTask;
        }
    }
}
