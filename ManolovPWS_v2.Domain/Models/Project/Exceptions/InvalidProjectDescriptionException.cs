using ManolovPWS_v2.Domain.Errors;

namespace ManolovPWS_v2.Domain.Models.Project.Exceptions
{
    public sealed class InvalidProjectDescriptionException(string message, string code) 
        : DomainException(message, code);
}
