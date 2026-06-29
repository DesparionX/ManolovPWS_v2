using ManolovPWS_v2.Domain.Errors;

namespace ManolovPWS_v2.Domain.Models.User.Exceptions
{
    public sealed class InvalidGenderException(string message, string code) 
        : DomainException(message, code);
}
