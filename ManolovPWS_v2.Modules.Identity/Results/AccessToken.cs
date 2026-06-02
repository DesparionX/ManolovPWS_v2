namespace ManolovPWS_v2.Modules.Identity.Results
{
    public sealed record AccessToken(string Token, DateTime ExpiresAtUtc);
}
