using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Motor.Inquiry.Application.DTOs;
using Motor.Inquiry.Application.Interfaces;

namespace Motor.Inquiry.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MotorInquiryController : ControllerBase
    {
        private readonly IMotorInquiryService _motorInquiryService;
        private readonly IInquiryService _inquiryService;

        public MotorInquiryController(IMotorInquiryService motorInquiryService, IInquiryService inquiryService)
        {
            _motorInquiryService = motorInquiryService;
            _inquiryService = inquiryService;
        }

        [HttpPost("inquire")]
        public async Task<IActionResult> InquireVehicle([FromBody] MotorInquiryRequestDto request)
        {
            var result = await _motorInquiryService.ProcessInquiryAsync(request);

            await _inquiryService.ProcessInquiryAsync(request);

            return Ok(result);
        }
    }
}