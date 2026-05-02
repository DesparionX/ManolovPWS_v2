namespace ManolovPWS_v2.Domain.Models.Project.Exceptions
{
    public sealed class InvalidProjectStackException(string message) : Exception(message)
    {
    }
}
