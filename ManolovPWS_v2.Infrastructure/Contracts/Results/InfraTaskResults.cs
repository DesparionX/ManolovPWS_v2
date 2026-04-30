using ManolovPWS_v2.Shared.Abstractions.Results;
using Microsoft.AspNetCore.Identity;

namespace ManolovPWS_v2.Infrastructure.Contracts.Results
{
    public static class InfraTaskResults
    {
        public static Result ToInfraTaskResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => new InfraError(e.Code, e.Description)).ToList());
        }
    }
}
