using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Motor.Inquiry.Application.Common.Exceptions;
using Motor.Inquiry.Application.DTOs;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Motor.Inquiry.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex, _env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IHostEnvironment env)
        {
            context.Response.ContentType = "application/json";

            // القيم الافتراضية للخطأ العام (500)
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred on the server.";

            // التعامل مع الاستثناءات المخصصة برمجياً وتحويل المفاتيح إلى رسائل واضحة
            switch (exception)
            {
                case BadRequestException ex:
                    statusCode = HttpStatusCode.BadRequest; // 400
                    message = ex.Message switch
                    {
                        "InvalidCitizen" => "Invalid National ID or Date of Birth",
                        "OwnershipMismatch" => "Ownership mismatch: The vehicle does not belong to this citizen",
                        _ => ex.Message
                    };
                    break;

                case NotFoundException ex:
                    statusCode = HttpStatusCode.NotFound; // 404
                    message = ex.Message switch
                    {
                        "VehicleNotFound" => "Vehicle not found",
                        _ => ex.Message
                    };
                    break;

                case UnauthorizedAccessException ex:
                    statusCode = HttpStatusCode.Unauthorized; // 401
                    message = ex.Message;
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var response = new ErrorResponse
            {
                Success = false,
                Message = message,
                Error = env.IsDevelopment() ? exception.Message : null,
                StackTrace = env.IsDevelopment() ? exception.StackTrace : null
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}