using IdentityServer.Domain.Entities; using Microsoft.EntityFrameworkCore; using Microsoft.EntityFrameworkCore.Metadata.Builders;  namespace IdentityServer.Infrastructure.Persistence.Configurations;  public class CompanyConfiguration : IEntityTypeConfiguration<Company> {     public void Configure(EntityTypeBuilder<Company> builder)     {         builder.HasKey(x => x.Id);         builder.Property(x => x.Id).ValueGeneratedNever();          builder.Property(t => t.Name)                .HasMaxLength(50)                .IsUnicode()                .IsRequired();

        builder.Property(t => t.CEOFirstName)
              .HasMaxLength(50)
              .IsUnicode();

        builder.Property(t => t.CEOLastName)
              .HasMaxLength(50)
              .IsUnicode();

        builder.Property(t => t.CEOCell)
              .HasMaxLength(20);

        builder.Property(t => t.EconomicCode)
              .HasMaxLength(20);

        builder.Property(t => t.RegisterNo)
              .HasMaxLength(20)
              .IsRequired();

        builder.Property(t => t.TellPhone)
              .IsRequired(false)
              .HasMaxLength(20);

        builder.Property(t => t.NationalCode)
               .HasMaxLength(15)
               .IsUnicode();

        builder.Property(t => t.Address)
          .HasMaxLength(1024)
          .IsUnicode();
    } } 