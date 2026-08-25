using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yaqeen.Application.Interfaces;
using Yaqeen.Application.DTOs;
using FluentValidation;



namespace Yaqeen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleInquiryController : ControllerBase
    {
        private readonly IVehicleInquiryService _vehicleInquiryService;

        public VehicleInquiryController(IVehicleInquiryService vehicleInquiryService)
        {
            _vehicleInquiryService = vehicleInquiryService;
        }

        [HttpPost("inquire")]
        public async Task<IActionResult>  Inquire([FromBody] VehicleInquiryRequestDto request)
        {

            var result = await _vehicleInquiryService.InquiryVehicleOwnershipAsync(request);

            return Ok(result);
        }

        
    }
}
