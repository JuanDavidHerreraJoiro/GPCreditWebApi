using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Identification)
               .HasMaxLength(10)
               .IsRequired();

            builder.Property(x => x.DocumentType)
               .HasMaxLength(10)
               .IsRequired();

            builder.Property(x => x.Name)
               .HasMaxLength(80)
               .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Address)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();
        }
    }
}
