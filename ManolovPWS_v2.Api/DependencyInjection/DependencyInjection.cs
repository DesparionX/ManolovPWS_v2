using ManolovPWS_v2.Api.Services;

namespace ManolovPWS_v2.Api.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IDispatcher, Dispatcher>();

            return services;
        }
    }
}
