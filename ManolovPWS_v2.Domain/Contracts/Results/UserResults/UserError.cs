using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Domain.Contracts.Results.UserResults
{
    public sealed record UserError(string Message, string Code) : IError;
}
