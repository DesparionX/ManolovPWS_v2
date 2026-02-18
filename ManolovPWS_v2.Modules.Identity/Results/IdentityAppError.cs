using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Modules.Identity.Results
{
    public sealed record IdentityAppError(string Message, string Code) : IError;
}
