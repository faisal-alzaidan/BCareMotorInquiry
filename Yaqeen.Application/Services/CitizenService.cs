using System.Linq;
using System.Threading.Tasks;
using Yaqeen.Application.Common;
using Yaqeen.Application.Common.Exceptions;
using Yaqeen.Application.DTOs;
using Yaqeen.Application.Interfaces;

namespace Yaqeen.Application.Services
{
    public class CitizenService : ICitizenService
    {
        private readonly ICitizenRepository _citizenRepository;

        public CitizenService(ICitizenRepository citizenRepository)
        {
            _citizenRepository = citizenRepository;
        }

        public async Task<ApiResponse<CitizenValidationResponseDto>> ValidateCitizenAsync(ValidateCitizenRequestDto request)
        {
            var citizen = await _citizenRepository.GetByNationalIdAsync(request.NationalId);

            if (citizen == null)
            {
                throw new NotFoundException("Citizen not found.");
            }

            if (citizen.DateOfBirth.Date != request.DateOfBirth.Date)
            {
                throw new BadRequestException("Date of birth does not match.");
            }

            return new ApiResponse<CitizenValidationResponseDto>
            {
                Success = true,
                Message = "Citizen validated successfully.",
                Data = new CitizenValidationResponseDto
                {
                    IsValid = true,
                    FullName = citizen.FullName
                }
            };
        }

        public async Task<ApiResponse<CitizenDetailsWithAddressesDto>> GetCitizenWithAddressesAsync(string nationalId)
        {
            var citizen = await _citizenRepository.GetCitizenWithAddressesAsync(nationalId);

            if (citizen == null)
            {
                throw new NotFoundException("Citizen not found.");
            }

            var dto = new CitizenDetailsWithAddressesDto
            {

                NationalId = citizen.NationalId,
                FullName = citizen.FullName,
                Addresses = citizen.Addresses.Select(a => new AddressDto
                {
                    Id = a.Id,
                    City = a.City,
                    District = a.District,
                    Street = a.Street,
                    PostalCode = a.PostalCode
                }).ToList()
            };
            return new ApiResponse<CitizenDetailsWithAddressesDto>
            {
                Success = true,
                Message = "Citizen addresses retrieved successfully.",
                Data = dto

            };
        }
    }
}