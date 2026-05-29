using ManolovPWS_v2.Api.Contracts.Identity;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Shared.Abstractions.Identity;

namespace ManolovPWS_v2.Api.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser<UserId>, CurrentUser>();
            services.AddScoped<IDispatcher, Dispatcher>();

            return services;
        }
    }
}
