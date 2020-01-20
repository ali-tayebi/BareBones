using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Ordering.Persistence;

namespace Ordering.API.Infrastructure.DB
{
    public class OrderingDbContextDesignFactory : IDesignTimeDbContextFactory<OrderingDbContext>
    {
        public OrderingDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<OrderingDbContext>();

            optionsBuilder.UseSqlServer(config["ConnectionStrings:OrderingDb"], sqlServerOptionsAction: o => o.MigrationsAssembly("Ordering.API"));

            return new OrderingDbContext(optionsBuilder.Options);
        }
    }
}
