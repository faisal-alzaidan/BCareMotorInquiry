using Yaqeen.Application.Interfaces;
using Yaqeen.Application.Services;
using Yaqeen.Infrastructure.Repositories;
using Yaqeen.Infrastructure.Services;

namespace Yaqeen.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddYaqeenApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICitizenRepository, CitizenRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IVehicleInquiryService, VehicleInquiryService>();
            services.AddScoped<ICitizenService, CitizenService>();
            services.AddMemoryCache();
            services.AddSingleton<IVehicleLookupCacheService, VehicleLookupCacheService>();

            return services;
        }
    }
}