using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Infrastructure.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ManolovPWS_v2.Infrastructure.DependencyInjection
{
    public static class RepositoryInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            return services;
        }
    }
}
