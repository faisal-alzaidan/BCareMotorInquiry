using System;
using System.Collections.Generic;
using System.Text;

namespace Yaqeen.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        public string NationalId { get; set; } = string.Empty;

        public Citizens Citizen { get; set; } = null!;
    }
}
