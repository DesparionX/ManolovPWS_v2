using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
using ManolovPWS_v2.Modules.Identity.User.Auth.Token;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.Admin
{
    public sealed record RevokeUserPermissionCommand(string UserId, string Permission) : ICommand;

    public sealed class RevokeUserPermissionCommandHandler(
        IAuthorizationService authorizationService,
        IRefreshTokensService refreshTokenService
        ) : ICommandHandler<RevokeUserPermissionCommand>
    {
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly IRefreshTokensService _refreshTokenService = refreshTokenService;

        public async Task<ITaskResult> HandleAsync(RevokeUserPermissionCommand cmd, CancellationToken cancellationToken = default)
        {
            var result = await _authorizationService.RevokeUserPermissionAsync(cmd.UserId, cmd.Permission, cancellationToken);
            if (!result.IsSuccess)
                return Result.Failure([.. result.Errors]);

            await _refreshTokenService.RevokeAllUserTokensAsync(UserId.From(cmd.UserId), cancellationToken);

            return Result.Success();
        }
    }
}
