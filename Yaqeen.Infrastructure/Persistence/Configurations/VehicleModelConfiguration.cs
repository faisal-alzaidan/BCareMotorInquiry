using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yaqeen.Domain.Entities;

namespace Yaqeen.Infrastructure.Persistence.Configurations
{
    internal class VehicleModelConfiguration : IEntityTypeConfiguration<VehicleModel>
    {
        public void Configure(EntityTypeBuilder<VehicleModel> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasData(
                new VehicleModel { Id = 1, MakeId = 1, NameAr = "كامري", NameEn = "Camry" },
                new VehicleModel { Id = 2, MakeId = 2, NameAr = "إلنترا", NameEn = "Elantra" }
            );
        }
    }
}