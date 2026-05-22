using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManolovPWS_v2.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure
            (
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionString)
        {
            services.AddDatabase(connectionString);
            services.AddCustomServices();
            services.AddJwt(configuration);
            services.AddUserIdentity();
            services.AddRepositories();
            services.AddFactories();
            return services;
        }
    }
}
