using IdentityServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Infrastructure.Persistence.Configurations;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.Property(t => t.Name)
        .HasMaxLength(50)
        .IsRequired()
        .IsUnicode();

        builder.Property(t => t.LocalName)
        .HasMaxLength(50)
        .IsRequired()
        .IsUnicode();

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.ChildrenRoles)
            .HasForeignKey(x => x.ParentId);
    }
}
