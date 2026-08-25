using System;
using System.Collections.Generic;
using System.Text;
using Yaqeen.Domain.Entities;

namespace Yaqeen.Application.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicles?> GetBySequenceNumberAsync(string sequenceNumber);
        Task<Vehicles?> GetByPlateAsync(string plateNumber, string plateLetters);
        Task<bool> VerifyOwnershipAsync(string sequenceNumber, string nationalId);
    }
}
