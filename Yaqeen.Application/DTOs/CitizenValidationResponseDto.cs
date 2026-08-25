using System;
using System.Collections.Generic;
using System.Text;

namespace Yaqeen.Application.DTOs
{
    public class CitizenValidationResponseDto
    {
        public bool IsValid { get; set; }
        public string? FullName { get; set; }
    }
}
