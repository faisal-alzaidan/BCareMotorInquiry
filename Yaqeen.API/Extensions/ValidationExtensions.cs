using FluentValidation;
using FluentValidation.AspNetCore;
using Yaqeen.Application.Validators;

namespace Yaqeen.API.Extensions
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddYaqeenValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<VehicleInquiryRequestDtoValidator>();
            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}