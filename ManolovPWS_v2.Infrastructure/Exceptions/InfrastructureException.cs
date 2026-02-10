namespace ManolovPWS_v2.Infrastructure.Exceptions
{
    public sealed class InfrastructureException(string message, string code) : Exception($"{message}, {code}")
    {
    }
}
