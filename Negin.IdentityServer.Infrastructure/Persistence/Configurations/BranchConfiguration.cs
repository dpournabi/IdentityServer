using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Infrastructure.Persistence.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.HasKey(x=> x.Id);   
        builder.Property(x=>x.Id).ValueGeneratedNever();

        builder.Property(t => t.Name)
           .HasMaxLength(50)
           .IsRequired()
           .IsUnicode();

        builder.Property(t => t.CompanyId)
               .IsRequired();
    }
}
