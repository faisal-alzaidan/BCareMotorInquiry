using System;
using System.Collections.Generic;
using System.Text;

namespace Yaqeen.Application.DTOs
{
    public class GetVehicleByPlateRequestDto
    {
        public string PlateNumber { get; set; } = string.Empty;
        public string PlateLetters { get; set; } = string.Empty;
    }
}
