using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Domain.Entities
{
    public class VehicleInquiryCache
    {
        public int Id { get; set; }
        public string CacheKey { get; set; }
        public string? SequenceNumber { get; set; }
        public string? PlateLetters { get; set; }
        public string? PlateNumber { get; set; }
        public string ResponseJson { get; set; }
        public DateTime InquiryDate { get; set; }
    }
}
