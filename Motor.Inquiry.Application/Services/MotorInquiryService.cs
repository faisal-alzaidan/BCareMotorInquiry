using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Motor.Inquiry.Application.Common.Exceptions;
using Motor.Inquiry.Application.DTOs;
using Motor.Inquiry.Application.Interfaces;
using Motor.Inquiry.Application.Resources;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Motor.Inquiry.Application.Services
{
    public class MotorInquiryService : IMotorInquiryService
    {
        private readonly IYaqeenClientService _yaqeenClientService;
        private readonly IInsuranceOfferService _insuranceOfferService;
        private readonly IDistributedCache _distributedCache;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public MotorInquiryService(
            IYaqeenClientService yaqeenClientService,
            IInsuranceOfferService insuranceOfferService,
            IDistributedCache distributedCache,
            IStringLocalizer<SharedResource> localizer)
        {
            _yaqeenClientService = yaqeenClientService;
            _insuranceOfferService = insuranceOfferService;
            _distributedCache = distributedCache;
            _localizer = localizer;
        }

        public async Task<ApiResponse<MotorInquiryResponseDto>> ProcessInquiryAsync(MotorInquiryRequestDto request)
        {
            var isCitizenValid = await _yaqeenClientService.ValidateCitizenAsync(request.NationalId, request.DateOfBirth);

            if (!isCitizenValid)
            {
                throw new BadRequestException(_localizer["Invalid National ID or Date of Birth."]);
            }

            // Create a unique cache key based on vehicle identifiers
            string cacheKey = $"motor_inquiry_{request.SequenceNumber}_{request.PlateLetters}_{request.PlateNumber}";

            VehicleDetailsDto? vehicleDetails = null;

            // 1. Check Redis Cache first
            var cachedData = await _distributedCache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                vehicleDetails = JsonSerializer.Deserialize<VehicleDetailsDto>(cachedData);
            }

            // 2. If not found in Redis, fetch from external service (Yaqeen)
            if (vehicleDetails == null)
            {
                var yaqeenResponse = await _yaqeenClientService.VerifyVehicleAsync(request);

                if (!yaqeenResponse.Success || yaqeenResponse.Data == null)
                {
                    throw new NotFoundException(_localizer["VehicleNotFound"]);
                }

                vehicleDetails = yaqeenResponse.Data;

                // 3. Save the response in Redis with a 30-day expiration (TTL)
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    //AbsoluteExpirationRelativeToNow = System.TimeSpan.FromDays(30)
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
                };

                var serializedData = JsonSerializer.Serialize(vehicleDetails);
                await _distributedCache.SetStringAsync(cacheKey, serializedData, cacheOptions);
            }

            if (vehicleDetails.OwnerNationalId != request.NationalId)
            {
                throw new BadRequestException(_localizer["OwnershipMismatch"]);
            }

            var insuranceOffers = _insuranceOfferService.GetOffersForVehicle(vehicleDetails);

            return CreateResponse(
                success: true,
                message: _localizer["VehicleFetchedSuccess"],
                vehicle: vehicleDetails,
                offers: insuranceOffers
            );
        }

        private static ApiResponse<MotorInquiryResponseDto> CreateResponse(bool success, string message, VehicleDetailsDto? vehicle, List<InsuranceOfferDto> offers)
        {
            return new ApiResponse<MotorInquiryResponseDto>
            {
                Success = success,
                Message = message,
                Data = new MotorInquiryResponseDto
                {
                    Vehicle = vehicle,
                    InsuranceOffers = offers
                }
            };
        }
    }
}