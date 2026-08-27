using System;
using System.Collections.Generic;
using System.Text;
using Yaqeen.Domain.Entities;

namespace Yaqeen.Application.Interfaces
{
    public interface ICitizenRepository
    {
        Task<Citizens?> GetByNationalIdAsync(string nationalId);
        Task<Citizens?> GetCitizenWithAddressesAsync(string nationalId);
    }
}
