using System.Collections.Generic;
using System.Threading.Tasks;
using Motor.Inquiry.Application.Common.Exceptions;
using Motor.Inquiry.Application.DTOs;
using Motor.Inquiry.Application.Interfaces;
using Microsoft.Extensions.Localization;
using Motor.Inquiry.Application.Resources;

namespace Motor.Inquiry.Application.Services

{
    public class MotorInquiryService : IMotorInquiryService
    {

        private readonly IYaqeenClientService _yaqeenClientService;
        private readonly IInsuranceOfferService _insuranceOfferService;
        private readonly IVehicleInquiryCacheRepository _cacheRepository;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public MotorInquiryService(

        IYaqeenClientService yaqeenClientService,
        IInsuranceOfferService insuranceOfferService,
        IVehicleInquiryCacheRepository cacheRepository,
        IStringLocalizer<SharedResource> localizer)

        {
            _yaqeenClientService = yaqeenClientService;
            _insuranceOfferService = insuranceOfferService;
            _cacheRepository = cacheRepository;
            _localizer = localizer;

        }
        public async Task<ApiResponse<MotorInquiryResponseDto>> ProcessInquiryAsync(MotorInquiryRequestDto request)

        {
            var isCitizenValid = await _yaqeenClientService.ValidateCitizenAsync(request.NationalId, request.DateOfBirth);

            if (!isCitizenValid)

            {
                throw new BadRequestException(_localizer["Invalid National ID or Date of Birth."]);
            }

            // Check cache for existing response using the flexible cacheKey

            var vehicleDetails = await _cacheRepository.GetCachedResponseAsync(request.SequenceNumber,request.PlateLetters,request.PlateNumber);

            // if not found in cache, fetch from external service

            if (vehicleDetails == null)

            {
                var yaqeenResponse = await _yaqeenClientService.VerifyVehicleAsync(request);

                if (!yaqeenResponse.Success || yaqeenResponse.Data == null)

                {

                    throw new NotFoundException(_localizer["VehicleNotFound"]);

                }

                vehicleDetails = yaqeenResponse.Data;

                // save the response in the cache using the flexible cacheKey

                await _cacheRepository.SetCacheAsync(request.SequenceNumber,request.PlateLetters, request.PlateNumber, vehicleDetails);

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