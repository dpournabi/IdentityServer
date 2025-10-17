//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using IdentityServer.Domain.Entities;

//namespace IdentityServer.Infrastructure.Persistence.Configurations;

//public class ClientConfiguration : IEntityTypeConfiguration<Client>
//{
//    public void Configure(EntityTypeBuilder<Client> builder)
//    {
//        builder.HasKey(t => t.Id);
//        builder.Property(t => t.Id).ValueGeneratedNever();

//        builder.Property(t => t.ClientName)
//               .HasMaxLength(50)
//               .IsUnicode()
//               .IsRequired();
//    }
//}
