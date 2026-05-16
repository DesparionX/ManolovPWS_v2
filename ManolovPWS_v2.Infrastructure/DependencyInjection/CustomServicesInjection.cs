using ManolovPWS_v2.Infrastructure.Contracts.Authentication;
using ManolovPWS_v2.Infrastructure.Contracts.Authorization;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authentication;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
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
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            return services;
        }
    }
}
