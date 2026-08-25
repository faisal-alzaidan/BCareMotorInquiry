using System;
using System.Collections.Generic;
using System.Text;
using Yaqeen.Application.DTOs;

namespace Yaqeen.Application.Interfaces
{
    public interface IVehicleInquiryService
    {
        Task<ApiResponse<VehicleInquiryResponseDto>> InquiryVehicleOwnershipAsync(VehicleInquiryRequestDto request);
    }
}
