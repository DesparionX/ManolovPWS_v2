using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
using ManolovPWS_v2.Modules.Identity.User.Auth.Token;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.Admin
{
    public sealed record RemoveUserFromRoleCommand(string UserId, string Role) : ICommand;

    public sealed class RemoveUserFromRoleCommandHandler(
        IAuthorizationService authorizationService,
        IRefreshTokensService refreshTokensService
        ) : ICommandHandler<RemoveUserFromRoleCommand>
    {
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly IRefreshTokensService _refreshTokensService = refreshTokensService;

        public async Task<ITaskResult> HandleAsync(RemoveUserFromRoleCommand cmd,  CancellationToken cancellationToken = default)
        {
            var result = await _authorizationService.RemoveUserFromRoleAsync(cmd.UserId, cmd.Role, cancellationToken);
            if (!result.IsSuccess)
                return Result.Failure([.. result.Errors]);

            await _refreshTokensService.RevokeAllUserTokensAsync(UserId.From(cmd.UserId), cancellationToken);

            return Result.Success();
        }
    }
}
