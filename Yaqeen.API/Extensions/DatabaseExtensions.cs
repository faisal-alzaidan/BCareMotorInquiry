using Microsoft.EntityFrameworkCore;
using Yaqeen.Infrastructure.Persistence;

namespace Yaqeen.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddYaqeenDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<YaqeenDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}