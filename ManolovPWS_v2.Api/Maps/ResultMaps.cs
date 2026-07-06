using ManolovPWS_v2.Api.Errors;
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
            var errorList = errors.Select(e => new ApiError(e.Code, e.Message)).ToList();
            var primaryError = errorList[0]
                ?? throw new InvalidOperationException("Errors collection is empty.");

            return primaryError.Code switch
            {
                ErrorCodes.ActionFailed => new BadRequestObjectResult(errorList),
                ErrorCodes.ValidationError => new BadRequestObjectResult(errorList),
                ErrorCodes.Unauthorized => new UnauthorizedObjectResult(errorList),
                ErrorCodes.Forbidden => new ObjectResult(errorList) { StatusCode = StatusCodes.Status403Forbidden },
                ErrorCodes.NotFound => new NotFoundObjectResult(errorList),
                ErrorCodes.Conflict => new ConflictObjectResult(errorList),
                _ => new BadRequestObjectResult(errorList)
            };
        }
    }
}
