using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Infrastructure.Persistence.Configurations;

public class PlanTypeConfiguration : IEntityTypeConfiguration<PlanType>
{
    public void Configure(EntityTypeBuilder<PlanType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(t => t.Title)
             .HasMaxLength(50)
             .IsRequired()
             .IsUnicode();

        builder.Property(t => t.TitleEn)
             .HasMaxLength(20)
             .IsRequired();
    }
}
