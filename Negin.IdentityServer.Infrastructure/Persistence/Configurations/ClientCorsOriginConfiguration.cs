//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace IdentityServer.Infrastructure.Persistence.Configurations;

//public class ClientCorsOriginConfiguration : IEntityTypeConfiguration<Domain.Entities.ClientCorsOrigin>
//{
//    public void Configure(EntityTypeBuilder<Domain.Entities.ClientCorsOrigin> builder)
//    {
//        builder.Property(t => t.Origin)
//             .HasMaxLength(150)
//             .IsRequired();

//        builder.Property(t => t.ClientId).IsRequired();
//    }
//}
