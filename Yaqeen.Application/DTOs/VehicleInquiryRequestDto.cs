using System;
using System.Collections.Generic;
using System.Text;

namespace Yaqeen.Application.DTOs
{
    public class VehicleInquiryRequestDto
    {
        public string NationalId { get; set; } = string.Empty;
        public string SequenceNumber { get; set; } = string.Empty;

    }
}
