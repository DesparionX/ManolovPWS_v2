using ManolovPWS_v2.Modules.Identity.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Auth.Token
{
    public interface ITokenProvider
    {
        AccessToken GenerateAccessToken(TokenRequest request);
    }
}
