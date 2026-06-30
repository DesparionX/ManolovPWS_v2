using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
using ManolovPWS_v2.Modules.Identity.User.Auth.Token;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.ManageTokens
{
    public sealed record RefreshAccessTokenCommand(string RefreshToken) : ICommand<TokenPairResponse>;

    public sealed class RefreshAccessTokenCommandHandler(
        IRefreshTokensService refreshTokensService,
        ITokenProvider tokenProvider,
        IUserRepository userRepository,
        IAuthorizationService authorizationService) 
        : ICommandHandler<RefreshAccessTokenCommand, TokenPairResponse>
    {
        private readonly IRefreshTokensService _refreshTokensService = refreshTokensService;
        private readonly ITokenProvider _tokenProvider = tokenProvider;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAuthorizationService _authorizationService = authorizationService;

        public async Task<ITaskResult<TokenPairResponse>> HandleAsync(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _refreshTokensService.ValidateTokenAsync(request.RefreshToken, cancellationToken);
            if (!validationResult.IsSuccess)
                return Result<TokenPairResponse>.Failure([IdentityAppErrors.RefreshTokenInvalid]);

            var userResult = await _userRepository.FindByIdAsync(validationResult.Value.UserId, cancellationToken);
            if (!userResult.IsSuccess)
                return Result<TokenPairResponse>.Failure([IdentityAppErrors.UserNotFound]);

            var user = userResult.Value;
            var userRoles = await _authorizationService.GetUserRolesAsync(user.Id.Value.ToString(), cancellationToken);
            var userPermissions = await _authorizationService.GetUserPermissionsAsync(user.Id.Value.ToString(), cancellationToken);

            var tokenRequest = new TokenRequest(
                Id: user.Id.Value,
                UserName: user.UserName.Value,
                Email: user.Email.Value,
                Roles: userRoles,
                Permissions: userPermissions
                );

            var newAccessToken = _tokenProvider.GenerateAccessToken(tokenRequest);

            var revokeResult = await _refreshTokensService.RevokeTokenAsync(validationResult.Value.TokenHash, cancellationToken);
            if (!revokeResult.IsSuccess)
                return Result<TokenPairResponse>.Failure([IdentityAppErrors.FailedToRevokeRefreshToken]);

            var newRefreshTokenResult = await _refreshTokensService.CreateTokenAsync(user.Id, cancellationToken);
            if (!newRefreshTokenResult.IsSuccess)
                return Result<TokenPairResponse>.Failure([IdentityAppErrors.FailedToCreateNewRefreshToken]);

            var newRefreshToken = newRefreshTokenResult.Value;

            var tokenPair = new TokenPairResponse(AccessToken: newAccessToken, RefreshToken: newRefreshToken);

            return Result<TokenPairResponse>.Success(tokenPair);
        }
    }
}
