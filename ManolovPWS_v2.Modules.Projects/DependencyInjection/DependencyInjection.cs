using Microsoft.Extensions.DependencyInjection;

namespace ManolovPWS_v2.Modules.Projects.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectModule(this IServiceCollection services)
        {
            services.AddHandlers();

            return services;
        }
    }
}
