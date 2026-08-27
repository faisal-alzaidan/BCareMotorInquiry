using System.Threading.Tasks;
using Yaqeen.Application.Common;
using Yaqeen.Application.DTOs;

namespace Yaqeen.Application.Interfaces
{
    public interface ICitizenService
    {
        Task<ApiResponse<CitizenValidationResponseDto>> ValidateCitizenAsync(ValidateCitizenRequestDto request);
        Task<ApiResponse<CitizenDetailsWithAddressesDto>> GetCitizenWithAddressesAsync(string nationalId);
    }
}