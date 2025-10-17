using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(t => t.UserName)
               .HasMaxLength(10)
               .IsRequired()
               .IsUnicode();

        builder.Property(t => t.NormalizedUserName)
               .HasMaxLength(10)
               .IsRequired()
               .IsUnicode();

        builder.Property(t => t.Email)
               .HasMaxLength(50);

        builder.Property(t => t.NormalizedEmail)
               .HasMaxLength(50);

        builder.Property(t => t.IsActive)
               .IsRequired();
    }
}
