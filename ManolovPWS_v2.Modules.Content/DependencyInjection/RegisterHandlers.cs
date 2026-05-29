using ManolovPWS_v2.Shared.Abstractions.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace ManolovPWS_v2.Modules.Content.DependencyInjection
{
    public static class RegisterHandlers
    {
        public static class ContentModuleAssembly { }
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            var assembly = typeof(ContentModuleAssembly).Assembly;

            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            return services;
        }
    }
}
