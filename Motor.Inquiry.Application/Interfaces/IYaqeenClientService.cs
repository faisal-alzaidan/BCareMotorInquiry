using System;
using System.Collections.Generic;
using System.Text;
using Motor.Inquiry.Application.DTOs;

namespace Motor.Inquiry.Application.Interfaces
{
    public interface IYaqeenClientService
    {
        Task<ApiResponse<VehicleDetailsDto>> VerifyVehicleAsync(MotorInquiryRequestDto request, CancellationToken cancellationToken = default);

        Task<bool> ValidateCitizenAsync(string nationalId, DateTime dateOfBirth, CancellationToken cancellationToken = default);
    }
}
