namespace ManolovPWS_v2.Modules.Identity.User.Auth.Authentication
{
    public sealed record TokenRequest(string Id, string UserName, string Email, IEnumerable<string> Roles);
}
