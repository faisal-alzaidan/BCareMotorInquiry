using System.Collections.Generic;
using System.Threading.Tasks;
using Motor.Inquiry.Application.Common.Exceptions;
using Motor.Inquiry.Application.DTOs;
using Motor.Inquiry.Application.Interfaces;
namespace Motor.Inquiry.Application.Services

{

    public class MotorInquiryService : IMotorInquiryService

    {

        private readonly IYaqeenClientService _yaqeenClientService;

        private readonly IInsuranceOfferService _insuranceOfferService;

        private readonly IVehicleInquiryCacheRepository _cacheRepository;



        public MotorInquiryService(

        IYaqeenClientService yaqeenClientService,

        IInsuranceOfferService insuranceOfferService,

        IVehicleInquiryCacheRepository cacheRepository)

        {

            _yaqeenClientService = yaqeenClientService;

            _insuranceOfferService = insuranceOfferService;

            _cacheRepository = cacheRepository;

        }



        public async Task<ApiResponse<MotorInquiryResponseDto>> ProcessInquiryAsync(MotorInquiryRequestDto request)

        {

            var isCitizenValid = await _yaqeenClientService.ValidateCitizenAsync(request.NationalId, request.DateOfBirth);



            if (!isCitizenValid)

            {

                throw new BadRequestException("Invalid National ID or Date of Birth.");

            }



            // 1. تحديد مفتاح الكاش بذكاء (إما رقم التسلسل أو دمج حروف وأرقام اللوحة)

            string cacheKey = !string.IsNullOrEmpty(request.SequenceNumber)

            ? request.SequenceNumber

            : $"{request.PlateLetters}_{request.PlateNumber}";



            // Check cache for existing response using the flexible cacheKey

            var vehicleDetails = await _cacheRepository.GetCachedResponseAsync(request.SequenceNumber,

            request.PlateLetters,

            request.PlateNumber);



            // if not found in cache, fetch from external service

            if (vehicleDetails == null)

            {

                var yaqeenResponse = await _yaqeenClientService.VerifyVehicleAsync(request);



                if (!yaqeenResponse.Success || yaqeenResponse.Data == null)

                {

                    throw new NotFoundException(yaqeenResponse.Message ?? "Vehicle not found.");

                }



                vehicleDetails = yaqeenResponse.Data;



                // save the response in the cache using the flexible cacheKey

                await _cacheRepository.SetCacheAsync(request.SequenceNumber,

                request.PlateLetters,

                request.PlateNumber,

                vehicleDetails);

            }



            if (vehicleDetails.OwnerNationalId != request.NationalId)

            {

                throw new BadRequestException("Ownership mismatch: The vehicle does not belong to this citizen.");

            }



            var insuranceOffers = _insuranceOfferService.GetOffersForVehicle(vehicleDetails);



            return CreateResponse(

            success: true,

            message: "Vehicle details fetched and insurance offers generated successfully.",

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