using ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels;

namespace ManolovPWS_v2.Modules.Identity.User.Features.SignInUser
{
    public sealed record SignInResponse(string AccessToken, AuthUserReadModel User, DateTime? ExpiresAt = default, string? RefreshToken = default);
}
