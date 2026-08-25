using System;
using System.Collections.Generic;
using System.Text;
using Yaqeen.Application.DTOs;
using Yaqeen.Domain.Entities;

namespace Yaqeen.Application.Interfaces
{
    public interface IVehicleService
    {
        Task<ApiResponse<VehicleDetailsDto>> GetBySequenceNumberAsync(string sequenceNumber);
        Task<ApiResponse<VehicleDetailsDto>> GetByPlateAsync(GetVehicleByPlateRequestDto request);
        Task<ApiResponse<bool>> VerifyOwnershipAsync(string sequenceNumber, string nationalId);
    }
}
