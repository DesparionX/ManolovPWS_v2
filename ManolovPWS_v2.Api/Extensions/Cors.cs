using ManolovPWS_v2.Api.Configuration;

namespace ManolovPWS_v2.Api.Extensions
{
    public static class Cors
    {
        public static IServiceCollection AddApiCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CorsSettings>(
                    configuration.GetSection("Cors"));

            var corsSettings = configuration
                .GetSection("Cors")
                .Get<CorsSettings>()
                ?? throw new InvalidOperationException("Cors settings not found in configuration.");

            services.AddCors(options =>
            {
                options.AddPolicy("Client", policy =>
                {
                    policy.WithOrigins(corsSettings.ClientUrl, corsSettings.LocalUrl)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            return services;
        }
    }
}
