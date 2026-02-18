using ManolovPWS_v2.Shared.Abstractions.Errors;
using ManolovPWS_v2.Shared.Abstractions.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Modules.Identity.Results
{
    public static class IdentityAppResults
    {
        public static ITaskResult Success()
            => new IdentityAppResult();

        public static ITaskResult<TResponse> Success<TResponse>(TResponse value)
            => new IdentityAppResult<TResponse>(value: value);

        public static ITaskResult Failure(IReadOnlyList<IdentityAppError> errors)
            => new IdentityAppResult(errors: errors);
    }
}
