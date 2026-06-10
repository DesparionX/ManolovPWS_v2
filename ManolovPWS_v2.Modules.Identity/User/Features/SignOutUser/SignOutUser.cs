using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authentication;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.SignOutUser
{
    public sealed record SignOutCommand(string RefreshToken) : ICommand;

    public sealed class SignOutCommandHandler(
        ICurrentUser<UserId> currentUser,
        IAuthService authService,
        IRefreshTokensService refreshTokensService)
        : ICommandHandler<SignOutCommand>
    {
        private readonly IAuthService _authService = authService;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;
        private readonly IRefreshTokensService _refreshTokensService = refreshTokensService;

        public async Task<ITaskResult> HandleAsync(SignOutCommand command, CancellationToken cancellationToken = default)
        {
            if (!_currentUser.IsAuthenticated)
                return Result.Failure([IdentityAppErrors.UserNotSignedIn]);

            var tokenValidationResult = await _refreshTokensService.ValidateTokenAsync(command.RefreshToken, cancellationToken);
            if (!tokenValidationResult.IsSuccess)
                return Result.Failure([IdentityAppErrors.RefreshTokenInvalid]);

            if (!_currentUser.Id.Equals(tokenValidationResult.Value.UserId))
                return Result.Failure([IdentityAppErrors.UserMissmatching]);

            var tokenHash = tokenValidationResult.Value.TokenHash;
            
            var revokeResult = await _refreshTokensService.RevokeTokenAsync(tokenHash, cancellationToken);
            if (!revokeResult.IsSuccess)
                return Result.Failure([IdentityAppErrors.FailedToRevokeRefreshToken, ..revokeResult.Errors]);

            await _authService.SignOutAsync(_currentUser.Id, cancellationToken);

            return Result.Success();
        }
    }
}
