using ManolovPWS_v2.Domain.Errors;

namespace ManolovPWS_v2.Domain.Models.User.Exceptions
{
    public class InvalidEmailException(string message, string code) 
        : DomainException(message, code);
}
