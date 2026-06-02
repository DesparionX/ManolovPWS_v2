using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Infrastructure.Contracts.Results
{
    public static class InfraErrors
    {
        // Authentication
        public static InfraError RefreshTokenInvalid => new(Message: "Refresh token is invalid.", Code: ErrorCodes.Unauthorized);
    }
}
