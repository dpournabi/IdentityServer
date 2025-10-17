using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Infrastructure.Persistence.Configurations;

public class LicenceConfiguration : IEntityTypeConfiguration<Licence>
{
    public void Configure(EntityTypeBuilder<Licence> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(t => t.DiscountValue)
            .HasPrecision(36,0);

        builder.Property(t => t.PlanId)
            .IsRequired();

        builder.Property(t => t.ApplicationId)
             .IsRequired();

        builder.Property(t => t.CompanyId)
                .IsRequired();

        builder.Property(t => t.StartDate)
          .IsRequired();

        builder.Property(t => t.EndDate)
            .IsRequired();

        builder.Property(t => t.PaymentStatus)
            .IsRequired();

        builder.Property(t => t.IsActive)
            .IsRequired();
    }
}
