using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Domain.Entities
{
    public class InquiryHistory
    {
        public int Id { get; set; }
        public string NationalId { get; set; } = string.Empty;
        public string? SequenceNumber { get; set; }
        public string? PlateLetters { get; set; }
        public string? PlateNumber { get; set; }    
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime InquiryDate { get; set; } = DateTime.UtcNow;
    }
}
