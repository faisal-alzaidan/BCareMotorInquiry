using System;
using System.Collections.Generic;
using System.Text;

namespace Yaqeen.Application.DTOs
{
    public class ValidateCitizenRequestDto
    {
        public string NationalId { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
