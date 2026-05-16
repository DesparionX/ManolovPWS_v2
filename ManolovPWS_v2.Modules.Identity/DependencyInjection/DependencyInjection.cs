using Microsoft.Extensions.DependencyInjection;

namespace ManolovPWS_v2.Modules.Identity.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services)
        {
            services.AddHandlers();

            return services;
        }
    }
}
