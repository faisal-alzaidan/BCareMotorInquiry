using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yaqeen.Domain.Entities;
using Yaqeen.Domain.Enums;

namespace Yaqeen.Infrastructure.Persistence.Configurations
{
    internal class CitizenConfiguration : IEntityTypeConfiguration<Citizens>
    {
        public void Configure(EntityTypeBuilder<Citizens> builder)
        {
           builder.HasKey(c=>c.NationalId);

            builder.HasOne(c => c.MaritalStatus)
                .WithMany(m => m.Citizens)
                .HasForeignKey(c => c.MaritalStatusId);

            builder.HasData(
                new Citizens
                {
                    NationalId = "1011223344",
                    FullName = "Faisal",
                    DateOfBirth = new DateTime(1995, 5, 15),
                    Gender = Gender.Male,
                    Nationality = "SA",
                    MaritalStatusId = 1
                },
                new Citizens
                {
                    NationalId = "1099887766",
                    FullName = "Sara",
                    DateOfBirth = new DateTime(1998, 10, 20),
                    Gender = Gender.Female,
                    Nationality = "SA",
                    MaritalStatusId = 2

                }
            );
        }
    }
}
