using ManolovPWS_v2.Infrastructure.Contracts.Authentication.JWT;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ManolovPWS_v2.Api.Extensions
{
    public static class Authentication
    {
        public static IServiceCollection AddAuthenticationDI(this IServiceCollection services, IConfiguration configuration)
        {

            services
                .AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    var jwt = configuration.GetSection("Jwt").Get<JwtSettings>();

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwt!.Issuer,
                        ValidAudience = jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt!.Key))
                    };
                });

            return services;
        }
    }
}
