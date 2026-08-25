using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Yaqeen.Domain.Entities;

namespace Yaqeen.Infrastructure.Persistence
{
    public class YaqeenDbContext : DbContext
    {
        public YaqeenDbContext(DbContextOptions<YaqeenDbContext> options) : base(options)
        {
        }

        public DbSet<Citizens> Citizens => Set<Citizens>();
        public DbSet<Vehicles> Vehicles => Set<Vehicles>();
        public DbSet<VehicleMake> VehicleMakes => Set<VehicleMake>();
        public DbSet<VehicleModel> VehicleModels => Set<VehicleModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicles>()
                .HasIndex(v => v.SequenceNumber);

            modelBuilder.Entity<Vehicles>()
                .HasIndex(v => new { v.PlateNumber, v.PlateLetters });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}