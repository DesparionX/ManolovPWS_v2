using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Auth.Token
{
    public interface IRefreshTokensService
    {
        public Task<ITaskResult<RefreshToken>> CreateTokenAsync(UserId userId, CancellationToken cancellationToken = default);
        public Task<ITaskResult<RefreshTokenValidationResult>> ValidateTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
        public Task<ITaskResult> RevokeTokenAsync(string hashedToken, CancellationToken cancellationToken = default);
        public Task<ITaskResult> RevokeAllUserTokensAsync(UserId userId, CancellationToken cancellationToken = default);
    }
}
