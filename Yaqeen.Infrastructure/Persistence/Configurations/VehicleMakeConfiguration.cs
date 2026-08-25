using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yaqeen.Domain.Entities;

namespace Yaqeen.Infrastructure.Persistence.Configurations
{
    internal class VehicleMakeConfiguration : IEntityTypeConfiguration<VehicleMake>
    {
        public void Configure(EntityTypeBuilder<VehicleMake> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasData(
                new VehicleMake { Id = 1, NameAr = "تويوتا", NameEn = "Toyota" },
                new VehicleMake { Id = 2, NameAr = "هونداي", NameEn = "Hyundai" }
            );
        }
    }
}