using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Application.DTOs
{
    public class InsuranceOfferDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string CoverageType { get; set; } = string.Empty;
        public decimal Price { get; set; } // use decimal for monetary values to avoid floating-point precision issues 
    
}
}
