using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Yaqeen.Domain.Entities;

namespace Yaqeen.Infrastructure.Persistence.Configurations
{
    internal class VehicleConfiguration : IEntityTypeConfiguration<Vehicles>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Vehicles> builder)
        {
            builder.HasKey(v => v.SequenceNumber);

            builder.HasOne(v => v.Make)
                .WithMany()
                .HasForeignKey(v => v.MakeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.Model)
                .WithMany()
                .HasForeignKey(v => v.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Vehicles
                {
                    SequenceNumber = "700123456",
                    PlateNumber = "1234",
                    PlateLetters = "SSH",
                    MakeId = 1,
                    ModelId = 1,
                    ModelYear = 2022,
                    Color = "White",
                    ChassisNumber = "JT112233445566778",
                    OwnerNationalId = "1011223344" 
                },
                new Vehicles
                {
                    SequenceNumber = "700987654",
                    PlateNumber = "5678",
                    PlateLetters = "HAl",
                    MakeId = 2,
                    ModelId = 2,
                    ModelYear = 2021,
                    Color = "Silver",
                    ChassisNumber = "KM887766554433221",
                    OwnerNationalId = "1099887766" 
                }
            );
        }
    }
}
