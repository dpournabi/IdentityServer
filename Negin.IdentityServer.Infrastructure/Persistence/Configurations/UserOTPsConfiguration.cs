using IdentityServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer.Infrastructure.Persistence.Configurations;

public class UserOTPsConfiguration : IEntityTypeConfiguration<UserOTPs>
{
    public void Configure(EntityTypeBuilder<UserOTPs> builder)
    {
        builder.Property(t => t.MessageBody)
            .HasMaxLength(255)
            .IsRequired()
            .IsUnicode();

        builder.Property(t => t.OtpCode)
              .HasMaxLength(100)
              .IsRequired();

        builder.Property(t => t.IsRecieved)
                .IsRequired();

        builder.Property(t => t.UserId)
                .IsRequired();

        builder.Property(t => t.ExpireDate)
               .IsRequired();

        builder.Property(t => t.TokenExpireDate)
               .IsRequired();

        builder.Property(t => t.AccessToken)
               .IsRequired(false);

        builder.Property(t => t.RefreshToken)
               .IsRequired(false);

        builder.Property(t => t.TokenType)
               .HasMaxLength(50)
               .IsRequired(false);
    }
}
