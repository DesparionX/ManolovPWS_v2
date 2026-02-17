namespace ManolovPWS_v2.Domain.Models.User.Exceptions
{
    public sealed class InvalidAddressException(string message) : Exception(message)
    {
    }
}
