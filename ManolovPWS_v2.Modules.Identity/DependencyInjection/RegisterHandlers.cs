using ManolovPWS_v2.Shared.Abstractions.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace ManolovPWS_v2.Modules.Identity.DependencyInjection
{
    public static class IdentityModuleAssembly { }

    public static class RegisterHandlers
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            var assembly = typeof(IdentityModuleAssembly).Assembly;

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
