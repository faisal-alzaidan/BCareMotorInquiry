using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Yaqeen.Application.DTOs;
using Yaqeen.Application.Interfaces;

namespace Yaqeen.API.Controllers
{
    [Route("api/yaqeen/vehicle")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("{sequenceNumber}")]
        public async Task<IActionResult> GetBySequenceNumber(string sequenceNumber)
        {
            var result = await _vehicleService.GetBySequenceNumberAsync(sequenceNumber);
            return Ok(result);
        }

        [HttpGet("plate")]
        public async Task<IActionResult> GetByPlate([FromQuery] GetVehicleByPlateRequestDto request)
        {
            // الفاليديشن يعمل أوتوماتيكياً عبر الـ Middleware ويرجع 400 لو فيه خطأ
            var result = await _vehicleService.GetByPlateAsync(request);
            return Ok(result);
        }

        [HttpGet("verify-ownership")]
        public async Task<IActionResult> VerifyOwnership([FromQuery] string sequenceNumber, [FromQuery] string nationalId)
        {
            var result = await _vehicleService.VerifyOwnershipAsync(sequenceNumber, nationalId);
            return Ok(result);
        }
    }
}