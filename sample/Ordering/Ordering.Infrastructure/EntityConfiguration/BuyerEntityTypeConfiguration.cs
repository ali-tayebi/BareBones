using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models.BuyerAggregate;

namespace Ordering.Infrastructure.EntityConfiguration
{
    class BuyerEntityTypeConfiguration
        : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> buyerConfiguration)
        {
            buyerConfiguration.ToTable("buyers", OrderingDbContext.DEFAULT_SCHEMA);

            buyerConfiguration.HasKey(b => b.Id);

            buyerConfiguration.Ignore(b => b.DomainEvents);

            buyerConfiguration.Property(b => b.Id)
                .UseHiLo("buyerseq", OrderingDbContext.DEFAULT_SCHEMA);

            buyerConfiguration.Property(b => b.IdentityGuid)
                .HasMaxLength(200)
                .IsRequired();

            buyerConfiguration.HasIndex("IdentityGuid")
              .IsUnique(true);

            buyerConfiguration.Property(b => b.Name);

            buyerConfiguration.HasMany(b => b.PaymentMethods)
               .WithOne()
               .HasForeignKey("BuyerId")
               .OnDelete(DeleteBehavior.Cascade);

            var navigation = buyerConfiguration.Metadata.FindNavigation(nameof(Buyer.PaymentMethods));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
