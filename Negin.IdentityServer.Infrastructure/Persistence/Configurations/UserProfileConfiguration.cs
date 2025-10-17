using IdentityServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Infrastructure.Persistence.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.Property(t => t.FirstName)
                  .HasMaxLength(100)
                  .IsRequired()
                  .IsUnicode();

        builder.Property(t => t.LastName)
               .HasMaxLength(100)
               .IsRequired()
               .IsUnicode();
    }
}
