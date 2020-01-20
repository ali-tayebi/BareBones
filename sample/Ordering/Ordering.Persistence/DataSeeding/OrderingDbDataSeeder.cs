using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BareBones.Persistence.EntityFramework.Migration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Ordering.Domain;
using Ordering.Domain.Models.BuyerAggregate;

namespace Ordering.Persistence.DataSeeding
{
    public class OrderingDbDataSeeder : IDbDataSeeder<OrderingDbContext>
    {
        private readonly ILogger<OrderingDbDataSeeder> _logger;
        private readonly IExecutionPolicy _policy;
        private readonly IDbDataProvider<IEnumerable<OrderStatus>> _orderStatusDataProvider;
        private readonly IDbDataProvider<IEnumerable<CardType>> _cardTypesDataProvider;

        public OrderingDbDataSeeder(
            ILogger<OrderingDbDataSeeder> logger,
            IExecutionPolicy policy,
            IDbDataProvider<IEnumerable<OrderStatus>> orderStatusDataProvider,
            IDbDataProvider<IEnumerable<CardType>> cardTypesDataProvider)
        {
            _logger = logger;
            _policy = policy;
            _orderStatusDataProvider = orderStatusDataProvider;
            _cardTypesDataProvider = cardTypesDataProvider;
        }

        public async Task SeedAsync(OrderingDbContext dbContext)
        {
            await _policy.ExecuteAsync(async () =>
            {
                using (dbContext)
                {
                    _logger.LogInformation("Start seeding data...");
                    dbContext.Database.Migrate();

                    if (!dbContext.CardTypes.Any())
                    {
                        dbContext.CardTypes.AddRange(_cardTypesDataProvider.GetData());
                        await dbContext.SaveChangesAsync();
                    }

                    if (!dbContext.OrderStatus.Any())
                    {
                        dbContext.OrderStatus.AddRange(_orderStatusDataProvider.GetData());
                    }

                    await dbContext.SaveChangesAsync();

                    _logger.LogInformation("Data seeding is done");
                }
            });
        }
    }


    public interface IExecutionPolicy
    {
        Task ExecuteAsync(Func<Task> action);
    }

    // TODO: Should be replaced with Polly later
    public class SimpleExecutionPolicy : IExecutionPolicy
    {
        public Task ExecuteAsync(Func<Task> action)
        {
            return action.Invoke();
        }
    }
}
