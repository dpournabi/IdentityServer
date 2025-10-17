using IdentityServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Infrastructure.Persistence.Configurations;

public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.Property(t => t.ClaimType)
            .HasMaxLength(50);

        builder.Property(t => t.ClaimValue)
              .HasMaxLength(50);

        builder.Property(t => t.UserId)
                .IsRequired();
    }
}
