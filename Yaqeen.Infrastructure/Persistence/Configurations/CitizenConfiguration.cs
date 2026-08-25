using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Yaqeen.Domain.Entities;

namespace Yaqeen.Infrastructure.Persistence.Configurations
{
    internal class CitizenConfiguration : IEntityTypeConfiguration<Citizens>
    {
        public void Configure(EntityTypeBuilder<Citizens> builder)
        {
           builder.HasKey(c=>c.NationalId);

            builder.HasData(
                new Citizens
                {
                    NationalId = "1011223344",
                    FullName = "Faisal",
                    DateOfBirth = new DateTime(1995, 5, 15),
                    Gender = "Male",
                    Nationality = "SA"
                },
                new Citizens
                {
                    NationalId = "1099887766",
                    FullName = "Sara",
                    DateOfBirth = new DateTime(1998, 10, 20),
                    Gender = "Female",
                    Nationality = "SA"
                }
            );
        }
    }
}
