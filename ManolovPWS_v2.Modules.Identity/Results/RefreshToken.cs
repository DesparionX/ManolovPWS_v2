namespace ManolovPWS_v2.Modules.Identity.Results
{
    public sealed record RefreshToken(string Token, DateTime ExpiresAtUtc);
}
