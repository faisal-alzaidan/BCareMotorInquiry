using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Application.DTOs
{
    public class VehicleDetailsDto 
    {
        public string? SequenceNumber { get; set; }
        public VehicleMakeDto? Make { get; set; }
        public VehicleModelDto? Model { get; set; }
        public int? ModelYear { get; set; }
        public string? Color { get; set; }
        public string? PlateNumber { get; set; }
        public string? PlateLetters { get; set; }
        public string? OwnerNationalId { get; set; }
    }
}
