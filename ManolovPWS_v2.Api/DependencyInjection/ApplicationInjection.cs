using ManolovPWS_v2.Modules.Content.DependencyInjection;
using ManolovPWS_v2.Modules.Identity.DependencyInjection;
using ManolovPWS_v2.Modules.Projects.DependencyInjection;

namespace ManolovPWS_v2.Api.DependencyInjection
{
    public static class ApplicationInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddIdentityModule();
            services.AddProjectModule();
            services.AddContentModule();
            
            return services;
        }
    }
}
