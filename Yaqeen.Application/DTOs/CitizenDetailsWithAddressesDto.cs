using System;
using System.Collections.Generic;
using System.Text;

namespace Yaqeen.Application.DTOs
{
    public class CitizenDetailsWithAddressesDto
    {
        public string NationalId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public List<AddressDto> Addresses { get; set; } = new();
    }
}
