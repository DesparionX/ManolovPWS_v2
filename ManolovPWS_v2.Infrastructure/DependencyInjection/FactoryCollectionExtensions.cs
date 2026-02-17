using ManolovPWS_v2.Domain.Contracts.Factories;
using ManolovPWS_v2.Infrastructure.Contracts.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace ManolovPWS_v2.Infrastructure.DependencyInjection
{
    public static class FactoryCollectionExtensions
    {
        public static IServiceCollection AddFactories(this IServiceCollection services)
        {
            services.AddScoped<IUserFactory, UserFactory>();
            services.AddScoped<IPostFactory, PostFactory>();
            services.AddScoped<IProjectFactory, ProjectFactory>();

            return services;
        }
    }
}
