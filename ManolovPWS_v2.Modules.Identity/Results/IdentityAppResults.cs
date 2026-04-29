using ManolovPWS_v2.Shared.Abstractions.Errors;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.Results
{
    public static class IdentityAppResults
    {
        public static ITaskResult Success()
            => new IdentityAppResult();

        public static ITaskResult<TResponse> Success<TResponse>(TResponse value)
            => new IdentityAppResult<TResponse>(value: value);

        public static ITaskResult Failure(IReadOnlyList<IError> errors)
            => new IdentityAppResult(errors: errors);

        public static ITaskResult<TResponse> Failure<TResponse>(IReadOnlyList<IError> errors)
            => new IdentityAppResult<TResponse>(errors: errors);

        public static IdentityAppResult FromResult(ITaskResult result)
            => (IdentityAppResult)(result.IsSuccess ?
                Success() : Failure(result.Errors ?? []));
    }
}
