using ManolovPWS_v2.Domain.Errors;

namespace ManolovPWS_v2.Domain.Models.User.Exceptions
{
    public class InvalidUriException(string message, string code) 
        : DomainException(message, code);
}
