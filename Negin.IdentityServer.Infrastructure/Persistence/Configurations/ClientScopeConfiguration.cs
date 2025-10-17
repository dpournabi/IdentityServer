//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore;

//namespace IdentityServer.Infrastructure.Persistence.Configurations;

//public class ClientScopeConfiguration : IEntityTypeConfiguration<Domain.Entities.ClientScope>
//{
//    public void Configure(EntityTypeBuilder<Domain.Entities.ClientScope> builder)
//    {
//        builder.Property(t => t.Scope)
//             .HasMaxLength(200)
//             .IsRequired();

//        builder.Property(t => t.ClientId).IsRequired();
//    }
//}
