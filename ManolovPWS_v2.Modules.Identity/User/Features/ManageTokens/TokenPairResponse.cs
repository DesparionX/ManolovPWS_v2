using ManolovPWS_v2.Modules.Identity.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.ManageTokens
{
    public sealed record TokenPairResponse(AccessToken AccessToken, RefreshToken RefreshToken);
}
