using ManolovPWS_v2.Infrastructure.Contracts.Auth;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace ManolovPWS_v2.Infrastructure.DependencyInjection
{
    public static class CustomServicesInjection
    {
        public static IServiceCollection AddCustomServices
            (
            this IServiceCollection services)
        {
            // Custom services registration goes here
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
