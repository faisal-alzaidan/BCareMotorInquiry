using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Motor.Inquiry.API.Extensions; 

public static class RateLimitingExtensions
{
    public static IServiceCollection AddAppRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("fixed", opt =>
            {
                opt.PermitLimit = 5;                  
                opt.Window = TimeSpan.FromSeconds(10); 
                opt.QueueLimit = 2;                  
            });
        });

        return services;
    }
}