using ManolovPWS_v2.Domain.Errors;

namespace ManolovPWS_v2.Domain.Models.User.Exceptions
{
    public class InvalidUserIdException(string message, string code) 
        : DomainException(message, code);
}
