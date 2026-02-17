using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManolovPWS_v2.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure
            (
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionString)
        {
            services.AddDatabase(configuration, connectionString);
            services.AddUserIdentity();
            services.AddRepositories();
            services.AddFactories();
            
            return services;
        }
    }
}
