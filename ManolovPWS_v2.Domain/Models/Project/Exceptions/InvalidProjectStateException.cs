namespace ManolovPWS_v2.Domain.Models.Project.Exceptions
{
    public sealed class InvalidProjectStateException(string message) : Exception(message)
    {
    }
}
