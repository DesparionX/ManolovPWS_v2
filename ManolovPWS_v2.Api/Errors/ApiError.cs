using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Api.Errors
{
    public sealed record ApiError(string Code, string Message) : IError;
}
