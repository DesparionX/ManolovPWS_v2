namespace ManolovPWS_v2.Modules.Identity.Exceptions
{
    public sealed class IdentityAppException(string message, string code) : Exception($"{message}, {code}")
    {
    }
}
