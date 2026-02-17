namespace ManolovPWS_v2.Domain.Models.User.Exceptions
{
    public sealed class InvalidProfessionException(string message) : Exception(message)
    {
    }
}
