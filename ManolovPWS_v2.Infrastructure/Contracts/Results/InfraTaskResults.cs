using Microsoft.AspNetCore.Identity;

namespace ManolovPWS_v2.Infrastructure.Contracts.Results
{
    public static class InfraTaskResults
    {
        public static InfraTaskResult Success() => new();

        public static InfraTaskResult Failure(IEnumerable<IdentityError> errors)
            => new(errors.Select(e => new InfraError(e.Code, e.Description)).ToList());

        public static InfraTaskResult ToInfraTaskResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Success()
                : Failure(result.Errors);
        }
    }
}
