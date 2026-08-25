using Motor.Inquiry.Application.DTOs;
using Motor.Inquiry.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.Inquiry.Application.Services
{
    public class InsuranceOfferService: IInsuranceOfferService
    {
        public List<InsuranceOfferDto> GetOffersForVehicle (VehicleDetailsDto? vehicle)
        {
            return new List<InsuranceOfferDto>
            {
                new InsuranceOfferDto
                {
                    CompanyName = "Tawuniya",
                    CoverageType = "Comprehensive",
                    Price = 1250.50m
                },
                new InsuranceOfferDto
                {
                    CompanyName = "Tawuniya",
                    CoverageType = "TPL",
                    Price = 650.00m
                },
                new InsuranceOfferDto
                {
                    CompanyName = "ACIG",
                    CoverageType = "Comprehensive",
                    Price = 1100.00m
                },
                new InsuranceOfferDto
                {
                    CompanyName = "ACIG",
                    CoverageType = "TPL",
                    Price = 580.00m
                }
            };
        }
    }
}
