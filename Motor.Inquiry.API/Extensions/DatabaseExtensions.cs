using Microsoft.EntityFrameworkCore;
using Motor.Inquiry.Infrastructure.Persistence;

namespace Motor.Inquiry.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}