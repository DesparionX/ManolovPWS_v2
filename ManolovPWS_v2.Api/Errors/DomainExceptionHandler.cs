using ManolovPWS_v2.Domain.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Errors
{
    public sealed class DomainExceptionHandler(ILogger<DomainExceptionHandler> logger)
        : IExceptionHandler
    {
        private readonly ILogger<DomainExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
            )
        {
            if (exception is not DomainException domainException)
                return false;

            _logger.LogError(
                domainException,
                "Domain exception occured: {Code}",
                domainException.Code
                );

            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation failed.",
                Detail = domainException.Message,
                Instance = httpContext.Request.Path
            };

            problem.Extensions["code"] = domainException.Code;
            problem.Extensions["traceId"] = httpContext.TraceIdentifier;

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

            return true;
        }
    }
}
