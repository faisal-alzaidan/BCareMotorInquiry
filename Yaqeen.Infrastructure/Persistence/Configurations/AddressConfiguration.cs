using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Yaqeen.Domain.Entities;

namespace Yaqeen.Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {

            builder.HasKey(a => a.Id);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.District)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(a => a.PostalCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(a => a.NationalId)
                .IsRequired();

            builder.HasOne(a => a.Citizen)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.NationalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
    new Address
    {
        Id = 1,
        NationalId = "1011223344", // تأكد إن هذا رقم الهوية لمواطن موجود عندك بالسيد
        City = "الرياض",
        District = "الملز",
        Street = "شارع الستين",
        PostalCode = "12345"
    },
    new Address
    {
        Id = 2,
        NationalId = "1099887766",
        City = "أبها",
        District = "الخالدية",
        Street = "شارع الحزام",
        PostalCode = "54321"
    }
);
        }
    }
}