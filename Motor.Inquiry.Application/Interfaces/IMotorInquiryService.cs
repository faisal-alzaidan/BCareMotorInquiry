using Motor.Inquiry.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Application.Interfaces
{
    public interface IMotorInquiryService
    {
        Task<ApiResponse<MotorInquiryResponseDto>> ProcessInquiryAsync(MotorInquiryRequestDto request);
    }
}
