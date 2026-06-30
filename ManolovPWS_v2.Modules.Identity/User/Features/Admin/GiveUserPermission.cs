using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.Admin
{
    public sealed record GiveUserPermissionCommand(string UserId, string Permission) : ICommand;

    public sealed class GiveUserPermissionCommandHandler(
        IAuthorizationService authorizationService
        ) : ICommandHandler<GiveUserPermissionCommand>
    {
        private readonly IAuthorizationService _authorizationService = authorizationService;

        public async Task<ITaskResult> HandleAsync(GiveUserPermissionCommand cmd , CancellationToken cancellationToken = default)
        {
            var result = await _authorizationService.GiveUserPermissionAsync(cmd.UserId, cmd.Permission, cancellationToken);
            if (!result.IsSuccess)
                return Result.Failure([.. result.Errors]);

            return Result.Success();
        }
    }
}
