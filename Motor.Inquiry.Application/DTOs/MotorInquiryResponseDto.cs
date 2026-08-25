using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Application.DTOs
{
    public class MotorInquiryResponseDto
    {
        public VehicleDetailsDto? Vehicle { get; set; }
        public List<InsuranceOfferDto> InsuranceOffers { get; set; } = new();
    }
    
}
