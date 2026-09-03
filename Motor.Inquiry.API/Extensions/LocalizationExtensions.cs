using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Motor.Inquiry.API.Extensions;

public static class LocalizationExtensions
{
    public static IServiceCollection AddApiLocalization(this IServiceCollection services)
    {
        services.AddLocalization();

        return services;
    }

    public static IApplicationBuilder UseApiLocalization(this IApplicationBuilder app)
    {

        var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ar") };
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        };

        localizationOptions.RequestCultureProviders.Insert(0, new AcceptLanguageHeaderRequestCultureProvider());

        app.UseRequestLocalization(localizationOptions);

        return app;
    }
}