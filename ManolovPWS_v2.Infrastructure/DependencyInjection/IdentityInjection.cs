using ManolovPWS_v2.Infrastructure.Persistance;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ManolovPWS_v2.Infrastructure.DependencyInjection
{
    public static class IdentityInjection
    {
        public static IServiceCollection AddUserIdentity
            (
            this IServiceCollection services
            )
        {
            services.AddIdentity<DbUser, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";


                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
