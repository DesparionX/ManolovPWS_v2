using ManolovPWS_v2.Infrastructure.Contracts.Auth.JWT;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManolovPWS_v2.Infrastructure.DependencyInjection
{
    public static class JwtInjection
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtSettings>(config.GetSection("Jwt"));

            services.AddScoped<ITokenProvider, JwtProvider>();

            return services;
        }
    }
}
