using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Results.ProjectResults
{
    public sealed record ProjectError(string Message, string Code) : IError;
}
