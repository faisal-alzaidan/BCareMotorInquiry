using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Application.DTOs
{
    public class MotorInquiryRequestDto
    {
        public string NationalId { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } 
        public string? SequenceNumber { get; set; }
        public string? PlateLetters { get; set; } 
        public string? PlateNumber { get; set; }
    }
}
