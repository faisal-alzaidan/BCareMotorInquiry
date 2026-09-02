using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yaqeen.Application.DTOs;
using Yaqeen.Application.Interfaces;

namespace Yaqeen.API.Controllers
{
    [ServiceFilter(typeof(ApiKeyAuthAttribute))]
    [Route("api/yaqeen/citizen")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly ICitizenService _citizenService;

        public CitizenController(ICitizenService citizenService)
        {
            _citizenService = citizenService;
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateCitizen([FromBody] ValidateCitizenRequestDto request)
        {
            return Ok(await _citizenService.ValidateCitizenAsync(request));
        }

        [HttpGet("{nationalId}/addresses")]
        public async Task<IActionResult> GetCitizenWithAddresses(string nationalId)
        {
            var result = await _citizenService.GetCitizenWithAddressesAsync(nationalId);
            return Ok(result);


        }
    }
}