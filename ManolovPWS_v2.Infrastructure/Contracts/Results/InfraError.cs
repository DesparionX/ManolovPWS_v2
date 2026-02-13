using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Infrastructure.Contracts.Results
{
    public sealed record  InfraError(string Code, string Message) : IError;
}
