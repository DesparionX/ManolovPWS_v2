using ManolovPWS_v2.Domain.Errors;

namespace ManolovPWS_v2.Domain.Models.Post.Exceptions
{
    public sealed class InvalidPostPublishedDateException(string message, string code)
        : DomainException(message, code);
}
