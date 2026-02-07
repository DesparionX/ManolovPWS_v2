using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Results.PostResults
{
    public sealed record PostRecord(string Message, string Code) : IError;
}
