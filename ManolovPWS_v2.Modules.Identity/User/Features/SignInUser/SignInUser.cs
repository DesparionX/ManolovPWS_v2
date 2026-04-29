using ManolovPWS_v2.Modules.Identity.Exceptions;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authentication;
using ManolovPWS_v2.Modules.Identity.User.Maps;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.SignInUser
{
    public sealed record SignInUserQuery(string UserNameOrEmail, string Password) : IQuery<SignInResponse>;

    public sealed class SignInUserQueryHandler(IAuthService authService, ITokenProvider tokenProvider) 
        : IQueryHandler<SignInUserQuery, SignInResponse>
    {
        private readonly IAuthService _authService = authService;
        private readonly ITokenProvider _tokenProvider = tokenProvider;

        public async Task<ITaskResult<SignInResponse>> HandleAsync(SignInUserQuery request, CancellationToken cancellationToken = default)
        {
            var result = (await _authService.AuthenticateAsync(request.UserNameOrEmail, request.Password, cancellationToken))
                ?? throw new IdentityAppException("There is a problem with the authentication process.", "UnableToAuthenticate");

            if (!result.IsSuccess)
                return IdentityAppResults.Failure<SignInResponse>(result.Errors!);

            var user = result.Value;

            var tokenRequest = new TokenRequest(
                Id: user!.Id.Value,
                UserName: user.UserName.Value,
                Email: user.Email.Value
                );

            var token = _tokenProvider.GenerateAccessToken(tokenRequest);

            var response = new SignInResponse(token, user.ToAuthUserRm());

            return IdentityAppResults.Success(response);
        }
    }
}
