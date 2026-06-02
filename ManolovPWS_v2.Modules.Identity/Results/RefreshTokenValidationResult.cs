using ManolovPWS_v2.Domain.Models.User.Properties;

namespace ManolovPWS_v2.Modules.Identity.Results
{
    public sealed record RefreshTokenValidationResult(UserId UserId, string TokenHash);
}
