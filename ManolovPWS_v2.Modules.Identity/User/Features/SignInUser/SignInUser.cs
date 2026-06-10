using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authentication;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
using ManolovPWS_v2.Modules.Identity.User.Auth.Token;
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
        ITokenProvider tokenProvider,
        IRefreshTokensService refreshTokensService) 
        : ICommandHandler<SignInUserCommand, SignInResponse>
    {
        private readonly IAuthService _authService = authService;
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;
        private readonly ITokenProvider _tokenProvider = tokenProvider;
        private readonly IRefreshTokensService _refreshTokensService = refreshTokensService;

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

            var accessToken = _tokenProvider.GenerateAccessToken(tokenRequest);

            var refreshTokenResult = await _refreshTokensService.CreateTokenAsync(user.Id,cancellationToken);
            if(!refreshTokenResult.IsSuccess)
                return Result<SignInResponse>.Failure([IdentityAppErrors.UnableToAuthenticate, .. refreshTokenResult.Errors]);

            var response = new SignInResponse(accessToken, refreshTokenResult.Value, user.ToCompactUserRm());

            return Result<SignInResponse>.Success(response);
        }
    }
}
