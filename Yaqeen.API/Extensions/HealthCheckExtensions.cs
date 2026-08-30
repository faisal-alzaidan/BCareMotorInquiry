namespace Yaqeen.API.Extensions
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks();
            return services;
        }
    }
}