using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Infrastructure.Persistence.Configurations;

public class IndustryCompanyConfiguration : IEntityTypeConfiguration<IndustryCompany>
{
    public void Configure(EntityTypeBuilder<IndustryCompany> builder)
    {
        builder.Property(t => t.IndustryId)
               .IsRequired();

        builder.Property(t => t.CompanyId)
               .IsRequired();
    }
}
