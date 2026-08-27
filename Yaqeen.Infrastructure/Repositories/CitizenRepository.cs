using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Yaqeen.Application.Interfaces;
using Yaqeen.Domain.Entities;
using Yaqeen.Infrastructure.Persistence;

namespace Yaqeen.Infrastructure.Repositories
{
    public class CitizenRepository : ICitizenRepository
    {
        private readonly YaqeenDbContext _context;

        public CitizenRepository(YaqeenDbContext context)
        {
            _context = context;
        }

        public async Task<Citizens?> GetByNationalIdAsync(string nationalId)
        {
            return await _context.Citizens.FirstOrDefaultAsync(c=>c.NationalId == nationalId);
        }
        public async Task<Citizens?> GetCitizenWithAddressesAsync(string nationalId)
        {
            return await _context.Citizens
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.NationalId == nationalId);
        }
    }
}
