using ManolovPWS_v2.Shared.Abstractions.Errors;
using ManolovPWS_v2.Shared.Abstractions.Results;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Maps
{
    public static class ResultMaps
    {
        public static IActionResult ToActionResult(this ITaskResult result)
            => result.IsSuccess
                ? new OkResult()
                : WithErrors(result.Errors);

        public static IActionResult ToActionResult<T>(this ITaskResult<T> result)
            => result.IsSuccess
                ? new OkObjectResult(result.Value)
                : WithErrors(result.Errors);

        private static IActionResult WithErrors(IReadOnlyList<IError> errors)
        {
            var primaryError = errors[0]
                ?? throw new InvalidOperationException("Errors collection is empty.");

            return primaryError.Code switch
            {
                ErrorCodes.ActionFailed => new BadRequestObjectResult(errors),
                ErrorCodes.ValidationError => new BadRequestObjectResult(errors),
                ErrorCodes.Unauthorized => new UnauthorizedObjectResult(errors),
                ErrorCodes.Forbidden => new ObjectResult(errors) { StatusCode = StatusCodes.Status403Forbidden },
                ErrorCodes.NotFound => new NotFoundObjectResult(errors),
                ErrorCodes.Conflict => new ConflictObjectResult(errors),
                _ => new BadRequestObjectResult(errors)
            };
        }
    }
}
