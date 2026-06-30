using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.Admin
{
    public sealed record AddUserToRoleCommand(string UserId, string Role) : ICommand;

    public sealed class AddUserToRoleCommandHandler(
        IAuthorizationService authorizationService
        ) : ICommandHandler<AddUserToRoleCommand>
    {
        private readonly IAuthorizationService _authorizationService = authorizationService;

        public async Task<ITaskResult> HandleAsync(AddUserToRoleCommand cmd, CancellationToken cancellationToken = default)
        {
            var result = await _authorizationService.AddUserToRoleAsync(cmd.UserId, cmd.Role, cancellationToken);
            if (!result.IsSuccess)
                return Result.Failure([.. result.Errors]);

            return Result.Success();
        }
    }
}
