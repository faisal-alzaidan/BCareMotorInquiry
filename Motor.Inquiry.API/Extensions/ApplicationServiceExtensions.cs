using Motor.Inquiry.Application.Interfaces;
using Motor.Inquiry.Application.Services;
using Motor.Inquiry.Infrastructure.Repositories;
using Motor.Inquiry.Infrastructure.Services;
using Motor.Inquiry.Infrastructure.Settings;

namespace Motor.Inquiry.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Services & Repositories
            services.AddScoped<IInsuranceOfferService, InsuranceOfferService>();
            services.AddScoped<IMotorInquiryService, MotorInquiryService>();
            services.AddScoped<IInquiryHistoryService, InquiryHistoryRepository>();
            services.AddScoped<IInquiryService, InquiryService>();
            services.AddScoped<IVehicleInquiryCacheRepository, VehicleInquiryCacheRepository>();

            // Options & HttpClient
            services.Configure<YaqeenSettings>(configuration.GetSection("YaqeenSettings"));
            services.AddHttpClient<IYaqeenClientService, YaqeenHttpClientService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5250/");
            });


            return services;
        }
    }
}