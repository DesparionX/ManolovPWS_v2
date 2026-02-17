namespace ManolovPWS_v2.Domain.Models.User.Exceptions
{
    public sealed class InvalidSummaryException(string message) : Exception(message)
    {
    }
}
