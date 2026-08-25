using System;
using System.Collections.Generic;
using System.Text;

namespace Yaqeen.Application.DTOs
{
    public class VehicleInquiryResponseDto
    {
        public string? SequenceNumber { get; set; }
        public string? CitizenName { get; set; }
        public string? VehicleMake { get; set; }
        public string? VehicleModel { get; set; }
        public int? ModelYear { get; set; }
        public string? Color { get; set; }
        public string? PlateNumber { get; set; }
        public string? PlateLetters { get; set; }

    }
}
