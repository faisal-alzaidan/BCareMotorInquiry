using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Options;
using Motor.Inquiry.Application.Common.Exceptions;
using Motor.Inquiry.Application.DTOs;
using Motor.Inquiry.Application.Interfaces;
using Motor.Inquiry.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Localization;
using Motor.Inquiry.Application.Resources;

namespace Motor.Inquiry.Infrastructure.Services
{
    public class YaqeenHttpClientService : IYaqeenClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<YaqeenHttpClientService> _logger;
        private readonly YaqeenSettings _settings;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public YaqeenHttpClientService(HttpClient httpClient, ILogger<YaqeenHttpClientService> logger, IOptions<YaqeenSettings> settings, IStringLocalizer<SharedResource> localizer)
        {
            _httpClient = httpClient;
            _logger = logger;
            _settings = settings.Value;
            _localizer = localizer;

            if (!string.IsNullOrEmpty(_settings.ApiKey) && !_httpClient.DefaultRequestHeaders.Contains("X-Api-Key"))
            {
                _httpClient.DefaultRequestHeaders.Add("X-Api-Key", _settings.ApiKey);
            }
        }

        public async Task<ApiResponse<VehicleDetailsDto>> VerifyVehicleAsync(MotorInquiryRequestDto request, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpResponseMessage response;

                if (!string.IsNullOrWhiteSpace(request.SequenceNumber))
                {
                    var url = string.Format(_settings.Endpoints.VehicleBySequence, request.SequenceNumber);
                    response = await _httpClient.GetAsync(url, cancellationToken);
                }
                else
                {
                    var url = string.Format(_settings.Endpoints.VehicleByPlate, request.PlateNumber, request.PlateLetters);
                    response = await _httpClient.GetAsync(url, cancellationToken);
                }

                var rawJson = await response.Content.ReadAsStringAsync(cancellationToken);

                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // If Yaqeen returns 200 OK, map the vehicle object directly using the helper method
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = System.Text.Json.JsonSerializer.Deserialize<ApiResponse<VehicleDetailsDto>>(rawJson, options);


                    if (apiResponse != null && apiResponse.Data != null)
                    {
                        return CreateSuccessResponse(apiResponse.Data, apiResponse.Message);
                    }
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException(_localizer["YaqeenUnauthorized"]);
                }

                // If Yaqeen returns 400 Bad Request or 404 Not Found, use the error helper method
                return CreateErrorResponse(_localizer["YaqeenVehicleMismatch"]);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("The vehicle verification operation was cancelled.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while verifying vehicle with Yaqeen API.");
                return CreateErrorResponse(_localizer["YaqeenCommunicationError"]);
            }
        }



        public async Task<bool> ValidateCitizenAsync(string nationalId, DateTime dateOfBirth, CancellationToken cancellationToken = default)
        {
            try
            {
                var requestBody = new
                {
                    nationalId,
                    dateOfBirth = dateOfBirth.ToString("yyyy-MM-dd")
                };

                var response = await _httpClient.PostAsJsonAsync(_settings.Endpoints.ValidateCitizen, requestBody, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException(_localizer["YaqeenUnauthorized"]);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new NotFoundException(_localizer["CitizenNotFound"]);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new BadRequestException(_localizer["InvalidDateOfBirth"]);
                }

                return false;
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("The citizen validation operation was cancelled.");
                throw;
            }
            catch (Exception ex) when (ex is not NotFoundException && ex is not BadRequestException)
            {
                _logger.LogError(ex, "An unexpected error occurred while validating citizen with Yaqeen API.");
                throw;
            }
        }


        // Helper method to create a standardized success response.
        // Used to avoid code duplication and adhere to the DRY (Don't Repeat Yourself) principle.
        private static ApiResponse<VehicleDetailsDto> CreateSuccessResponse(VehicleDetailsDto vehicleDetails, string message)
        {
            return new ApiResponse<VehicleDetailsDto>
            {
                Success = true,
                Message = message,
                Data = vehicleDetails
            };
        }

        // Helper method to create a standardized error response.
        // Used to avoid code duplication and adhere to the DRY (Don't Repeat Yourself) principle.
        private static ApiResponse<VehicleDetailsDto> CreateErrorResponse(string message)
        {
            return new ApiResponse<VehicleDetailsDto>
            {
                Success = false,
                Message = message,
                Data = null
            };
        }

    }
}
