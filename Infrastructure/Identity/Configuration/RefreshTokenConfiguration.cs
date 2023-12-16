using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity.Models;

namespace Infrastructure.Identity.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token)
               .IsRequired();

            builder.Property(x => x.Expires)
               .IsRequired();

            builder.Property(x => x.Created)
               .IsRequired();

            builder.Property(x => x.CreatedByIp)
               .HasMaxLength(30)
               .IsRequired();

            builder.Property(x => x.Revoked);

            builder.Property(x => x.RevokedByIp)
               .HasMaxLength(30);

            builder.Property(x => x.ReplacedByToken);

            builder.Property(x => x.ReasonRevoked);
        }
    }
}
