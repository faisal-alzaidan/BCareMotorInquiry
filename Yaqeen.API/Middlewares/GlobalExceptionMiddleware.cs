using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Yaqeen.Application.Common;
using Yaqeen.Application.Common.Exceptions;
using Yaqeen.Application.DTOs;

namespace Yaqeen.API.Middlewares
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
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex, _env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IHostEnvironment env)
        {
            context.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred on the server. Please try again later.";

            if (exception is NotFoundException)
            {
                statusCode = HttpStatusCode.NotFound; // 404
                message = exception.Message;
            }
            else if (exception is BadRequestException)
            {
                statusCode = HttpStatusCode.BadRequest; // 400
                message = exception.Message;
            }

            context.Response.StatusCode = (int)statusCode;

            var response = new ErrorDetailsDto
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
                DetailedError = env.IsDevelopment() ? exception.StackTrace : null
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jsonResponse = JsonSerializer.Serialize(response, options);

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}