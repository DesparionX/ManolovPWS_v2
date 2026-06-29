using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Domain.Errors
{
    public abstract record DomainError(string Message, string Code) : IError;
}
