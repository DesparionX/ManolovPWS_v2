using ManolovPWS_v2.Api.Errors;

namespace ManolovPWS_v2.Api.DependencyInjection
{
    public static class ExceptionHandlersInjection
    {
        public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
        {
            services.AddExceptionHandler<DomainExceptionHandler>();
            services.AddExceptionHandler<InfrastructureExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            services.AddProblemDetails();

            return services;
        }
    }
}
