using FluentValidation;
using FluentValidation.AspNetCore;
using Motor.Inquiry.Application.Mappings;
using Motor.Inquiry.Application.Validators;

namespace Motor.Inquiry.API.Extensions
{
    public static class ValidationAndMappingExtensions
    {
        public static IServiceCollection AddValidationAndMapping(this IServiceCollection services)
        {
            // Validation
            services.AddValidatorsFromAssemblyContaining<MotorInquiryRequestDtoValidator>();
            services.AddFluentValidationAutoValidation();

            // AutoMapper
            services.AddAutoMapper(cfg => {
                cfg.AddProfile<InquiryMappingProfile>();
            });

            return services;
        }
    }
}