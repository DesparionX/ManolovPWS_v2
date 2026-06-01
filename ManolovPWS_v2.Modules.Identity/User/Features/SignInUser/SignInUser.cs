using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authentication;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
using ManolovPWS_v2.Modules.Identity.User.Maps;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.SignInUser
{
    public sealed record SignInUserCommand(string UserNameOrEmail, string Password) : ICommand<SignInResponse>;

    public sealed class SignInUserCommandHandler(
        IAuthService authService,
        IAuthorizationService authorizationService,
        ICurrentUser<UserId> currentUser,
        ITokenProvider tokenProvider) 
        : ICommandHandler<SignInUserCommand, SignInResponse>
    {
        private readonly IAuthService _authService = authService;
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;
        private readonly ITokenProvider _tokenProvider = tokenProvider;

        public async Task<ITaskResult<SignInResponse>> HandleAsync(SignInUserCommand request, CancellationToken cancellationToken = default)
        {
            if (_currentUser.IsAuthenticated)
                return Result<SignInResponse>.Failure([IdentityAppErrors.UserAlreadySignedIn]);

            var result = await _authService.AuthenticateAsync(request.UserNameOrEmail, request.Password, cancellationToken);

            if (result is null)
                return Result<SignInResponse>.Failure([IdentityAppErrors.UnableToAuthenticate]);

            if (!result.IsSuccess)
                return Result<SignInResponse>.Failure([IdentityAppErrors.InvalidCredentials, ..result.Errors]);

            var user = result.Value;
            var userRoles = await _authorizationService.GetUserRolesAsync(user.Id.Value.ToString(), cancellationToken);
            var userPermissions = await _authorizationService.GetUserPermissionsAsync(user.Id.Value.ToString(), cancellationToken);

            var tokenRequest = new TokenRequest(
                Id: user!.Id.Value,
                UserName: user.UserName.Value,
                Email: user.Email.Value,
                Roles: userRoles,
                Permissions: userPermissions
                );

            var token = _tokenProvider.GenerateAccessToken(tokenRequest);

            var response = new SignInResponse(token, user.ToAuthUserRm());

            return Result<SignInResponse>.Success(response);
        }
    }
}
