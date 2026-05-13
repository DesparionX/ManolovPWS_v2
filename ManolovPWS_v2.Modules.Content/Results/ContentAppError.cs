using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Modules.Content.Results
{
    public sealed record ContentAppError(string Message, string Code) : IError;
}
