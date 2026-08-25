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
                // رمي الاستثناء ليلقطه الميدل‌وير ويحوله تلقائياً إلى 404 Not Found
                throw new BadRequestException("Date of birth does not match.");
            }

            // رد النجاح مغلف ومرتب
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
    }
}