namespace ManolovPWS_v2.Modules.Identity.User.Auth.Authentication
{
    public interface ITokenProvider
    {
        string GenerateAccessToken(TokenRequest request);
    }
}
