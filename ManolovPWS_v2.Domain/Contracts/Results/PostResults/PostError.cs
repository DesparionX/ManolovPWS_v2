using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Domain.Contracts.Results.PostResults
{
    public sealed record PostError(string Message, string Code) : IError;
}
