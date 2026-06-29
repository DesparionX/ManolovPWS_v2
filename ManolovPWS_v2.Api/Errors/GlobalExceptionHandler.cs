using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Errors
{
    public sealed class GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger,
        IHostEnvironment environment
        ) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;
        private readonly IHostEnvironment _environment = environment;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
            )
        {
            _logger.LogError(
                exception,
                "Unhandled exception occured while processing {Method} {Path}",
                httpContext.Request.Method,
                httpContext.Request.Path
                );

            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Something went wrong.",
                Detail = "An unexpected error occured. Please try again later.",
                Instance = httpContext.Request.Path
            };

            problem.Extensions["traceId"] = httpContext.TraceIdentifier;

            if (_environment.IsDevelopment())
            {
                problem.Extensions["exception"] = exception.GetType().Name;
                problem.Extensions["message"] = exception.Message;
                problem.Extensions["stackTrace"] = exception.StackTrace;
            }

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

            return true;
        }
    }
}
