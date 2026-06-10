using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authentication;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.SignOutUser
{
    public sealed record SignOutCommand(string RefreshToken) : ICommand;

    public sealed class SignOutCommandHandler(ICurrentUser<UserId> currentUser, IAuthService authService)
        : ICommandHandler<SignOutCommand>
    {
        private readonly IAuthService _authService = authService;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<ITaskResult> HandleAsync(SignOutCommand command, CancellationToken cancellationToken = default)
        {
            if (!_currentUser.IsAuthenticated)
                return Result.Failure([IdentityAppErrors.UserNotSignedIn]);

            await _authService.SignOutAsync(_currentUser.Id, cancellationToken);

            return Result.Success();
        }
    }
}
