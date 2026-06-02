namespace ManolovPWS_v2.Modules.Identity.User.Auth.Token
{
    public sealed record TokenRequest(
        Guid Id,
        string UserName,
        string Email,
        IEnumerable<string>? Roles = default,
        IEnumerable<string>? Permissions = default);
}
