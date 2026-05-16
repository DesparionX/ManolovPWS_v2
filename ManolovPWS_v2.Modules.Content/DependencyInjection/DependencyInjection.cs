using ManolovPWS_v2.Modules.Content.CV.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ManolovPWS_v2.Modules.Content.DependencyInjection
{
    public static class ContentModuleDI
    {
        public static IServiceCollection AddContentModule(this IServiceCollection services)
        {
            services.AddScoped<ICVBuilder, CVBuilder>();
            services.AddHandlers();

            return services;
        }
    }
}
