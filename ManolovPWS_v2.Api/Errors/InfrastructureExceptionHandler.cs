using ManolovPWS_v2.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Errors
{
    public sealed class InfrastructureExceptionHandler(
        ILogger<InfrastructureExceptionHandler> logger,
        IHostEnvironment environment
        ) : IExceptionHandler
    {
        private readonly ILogger<InfrastructureExceptionHandler> _logger = logger;
        private readonly IHostEnvironment _environment = environment;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
            )
        {
            if (exception is not InfrastructureException infrastructureException)
                return false;

            _logger.LogError(
                infrastructureException,
                "Infrastructure exception occured: {Code}",
                infrastructureException.Code
                );

            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Validation failed.",
                Detail = _environment.IsDevelopment()
                    ? infrastructureException.Message
                    : "A technical error occured. Please try again later.",
                Instance = httpContext.Request.Path
            };

            problem.Extensions["code"] = infrastructureException.Code;
            problem.Extensions["traceId"] = httpContext.TraceIdentifier;

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

            return true;
        }
    }
}
