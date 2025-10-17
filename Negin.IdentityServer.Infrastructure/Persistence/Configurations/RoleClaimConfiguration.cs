using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Infrastructure.Persistence.Configurations;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.Property(t => t.ClaimType)
            .HasMaxLength(50);

        builder.Property(t => t.ClaimValue)
              .HasMaxLength(50);

        builder.Property(t => t.RoleId)
                .IsRequired();
    }
}
