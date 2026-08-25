using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Yaqeen.Application.Interfaces;
using Yaqeen.Domain.Entities;
using Yaqeen.Infrastructure.Persistence;

namespace Yaqeen.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly YaqeenDbContext _context;

        public VehicleRepository (YaqeenDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicles?> GetBySequenceNumberAsync(string sequenceNumber)
        {
            return await _context.Vehicles
                .Include(v => v.Make)
                .Include(v => v.Model)
                .FirstOrDefaultAsync(v => v.SequenceNumber == sequenceNumber);  
        }
        
        public async Task<Vehicles?> GetByPlateAsync(string plateNumber, string plateLetters)
        {
            return await _context.Vehicles
                .Include(v => v.Make)
                .Include(v => v.Model)
                .FirstOrDefaultAsync(v => v.PlateNumber == plateNumber && v.PlateLetters == plateLetters);
        }

        public async Task<bool> VerifyOwnershipAsync(string sequenceNumber, string nationalId)
        {
            return await _context.Vehicles.AnyAsync(v=>v.SequenceNumber == sequenceNumber && v.OwnerNationalId == nationalId);
        }


    }
}
