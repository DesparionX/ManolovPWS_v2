using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels;

namespace ManolovPWS_v2.Modules.Identity.User.Features.SignInUser
{
    public sealed record SignInResponse(
        AccessToken AccessToken,
        RefreshToken RefreshToken,
        AuthUserReadModel User
        );
}
