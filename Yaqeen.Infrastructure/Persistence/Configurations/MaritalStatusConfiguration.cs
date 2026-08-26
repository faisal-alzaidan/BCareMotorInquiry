using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yaqeen.Domain.Entities;

namespace Yaqeen.Infrastructure.Persistence.Configurations
{
    public class MaritalStatusConfiguration : IEntityTypeConfiguration<MaritalStatus>
    {
        public void Configure(EntityTypeBuilder<MaritalStatus> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NameAr)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.NameEn)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(
                new MaritalStatus { Id = 1, NameAr = "أعزب", NameEn = "Single" },
                new MaritalStatus { Id = 2, NameAr = "متزوج", NameEn = "Married" },
                new MaritalStatus { Id = 3, NameAr = "مطلق", NameEn = "Divorced" },
                new MaritalStatus { Id = 4, NameAr = "أرمل", NameEn = "Widowed" }
            );
        }
    }
}