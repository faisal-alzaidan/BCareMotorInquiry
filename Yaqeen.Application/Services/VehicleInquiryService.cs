using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Yaqeen.Application.Common.Exceptions;
using Yaqeen.Application.DTOs;
using Yaqeen.Application.Interfaces;
using Yaqeen.Domain.Entities;

namespace Yaqeen.Application.Services
{
    public class VehicleInquiryService : IVehicleInquiryService
    {
        private readonly ICitizenRepository _citizenRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleInquiryService(ICitizenRepository citizenRepository, IVehicleRepository vehicleRepository)
        {
            _citizenRepository = citizenRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ApiResponse<VehicleInquiryResponseDto>> InquiryVehicleOwnershipAsync(VehicleInquiryRequestDto request)
        {
            var citizen = await _citizenRepository.GetByNationalIdAsync(request.NationalId);
            if (citizen == null)
            {
                throw new NotFoundException("The citizen is not registered in the system.");
            }

            var vehicle = await _vehicleRepository.GetBySequenceNumberAsync(request.SequenceNumber);
            if (vehicle == null)
            {
                throw new NotFoundException("The vehicle is not registered in the system.");
            }

            var isOwner = await _vehicleRepository.VerifyOwnershipAsync(request.SequenceNumber, request.NationalId);
            if (!isOwner)
            {
                throw new BadRequestException("Sorry, the vehicle does not belong to this citizen.");
            }

            var responseData = new VehicleInquiryResponseDto
            {
                SequenceNumber = vehicle.SequenceNumber,
                CitizenName = citizen.FullName,
                VehicleMake = vehicle.Make?.NameEn ?? string.Empty,
                VehicleModel = vehicle.Model?.NameEn ?? string.Empty,
                ModelYear = vehicle.ModelYear,
                Color = vehicle.Color,
                PlateNumber = vehicle.PlateNumber,
                PlateLetters = vehicle.PlateLetters,
            };

            return new ApiResponse<VehicleInquiryResponseDto>
            {
                Success = true,
                Message = "The inquiry was successful, and the ownership details match.",
                Data = responseData
            };
        }

    }
    }

