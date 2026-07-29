using ManolovPWS_v2.Domain.Errors;

namespace ManolovPWS_v2.Domain.Models.Message.Exceptions
{
    public sealed class InvalidMessageSenderException(string message, string code)
        : DomainException(message, code);
}
