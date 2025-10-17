using IdentityServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Infrastructure.Persistence.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(t => t.Title)
            .HasMaxLength(100)
            .IsUnicode();

        builder.Property(t => t.PlanTypeId)
              .IsRequired();

        builder.Property(t => t.Amount)
              .IsRequired()
              .HasPrecision(36,0);

        builder.Property(t => t.ResourceSize)
                .IsRequired();
    }
}
