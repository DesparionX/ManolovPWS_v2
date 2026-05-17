using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authentication;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
using ManolovPWS_v2.Modules.Identity.User.Maps;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.SignInUser
{
    public sealed record SignInUserQuery(string UserNameOrEmail, string Password) : IQuery<SignInResponse>;

    public sealed class SignInUserQueryHandler(IAuthService authService, IAuthorizationService authorizationService, ITokenProvider tokenProvider) 
        : IQueryHandler<SignInUserQuery, SignInResponse>
    {
        private readonly IAuthService _authService = authService;
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly ITokenProvider _tokenProvider = tokenProvider;

        public async Task<ITaskResult<SignInResponse>> HandleAsync(SignInUserQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _authService.AuthenticateAsync(request.UserNameOrEmail, request.Password, cancellationToken);

            if (result is null)
                return Result<SignInResponse>.Failure([IdentityAppErrors.UnableToAuthenticate]);

            if (!result.IsSuccess)
                return Result<SignInResponse>.Failure([IdentityAppErrors.InvalidCredentials]);

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
