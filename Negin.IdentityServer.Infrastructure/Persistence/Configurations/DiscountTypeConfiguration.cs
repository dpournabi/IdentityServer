using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Infrastructure.Persistence.Configurations;

public class DiscountTypeConfiguration : IEntityTypeConfiguration<DiscountType>
{
    public void Configure(EntityTypeBuilder<DiscountType> builder)
    {
        builder.Property(t => t.TitleEn)
           .HasMaxLength(50);

        builder.Property(t => t.Title)
        .HasMaxLength(50)
        .IsUnicode();
    }
}
