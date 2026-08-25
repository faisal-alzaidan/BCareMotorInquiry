using Motor.Inquiry.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Application.Interfaces
{
    public interface IVehicleInquiryCacheRepository
    {
        Task<VehicleDetailsDto?> GetCachedResponseAsync(string? sequenceNumber, string? plateLetters, string? plateNumber);
        Task SetCacheAsync(string? sequenceNumber, string? plateLetters, string? plateNumber, VehicleDetailsDto vehicleDetails);
    }
}
