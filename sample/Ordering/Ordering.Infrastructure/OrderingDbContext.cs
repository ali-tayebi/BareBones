using System.Collections.Generic;
using BareBones.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain;
using Ordering.Domain.Models.BuyerAggregate;
using Ordering.Infrastructure.EntityConfiguration;

namespace Ordering.Infrastructure
{
    public class OrderingDbContext : DbContextBase
    {
        public const string DEFAULT_SCHEMA = "ordering";

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentMethod> Payments { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }

        public OrderingDbContext(DbContextOptions options) : base(options)
        {
        }

        public OrderingDbContext(
            DbContextOptions options,
            IEnumerable<IDbContextInterceptor> interceptors) : base(options, interceptors)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethodEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CardTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BuyerEntityTypeConfiguration());
        }
    }
}
