using ManolovPWS_v2.Shared.Authorization;

namespace ManolovPWS_v2.Api.Extensions
{
    public static class Authorization
    {
        public static IServiceCollection AddAuthorizationDI(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                foreach (var permission in Permissions.AllPermissions)
                {
                    options.AddPolicy(permission, policy =>
                        policy.RequireClaim(CustomClaimTypes.Permission, permission));
                }
            });
            return services;
        }
    }
}
