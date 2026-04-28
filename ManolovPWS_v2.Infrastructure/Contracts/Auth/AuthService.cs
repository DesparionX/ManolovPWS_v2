using ManolovPWS_v2.Infrastructure.Contracts.Results;
using ManolovPWS_v2.Infrastructure.Exceptions;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authentication;
using ManolovPWS_v2.Shared.Abstractions.Results;
using Microsoft.AspNetCore.Identity;

namespace ManolovPWS_v2.Infrastructure.Contracts.Auth
{
    public sealed class AuthService(SignInManager<DbUser> signInManager) : IAuthService
    {
        private readonly SignInManager<DbUser> _signInManager = signInManager;

        public async Task<ITaskResult> AuthenticateAsync(string emailOrUserName, string password, bool isPersistent = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _signInManager.UserManager.FindByEmailAsync(emailOrUserName) 
                ?? await _signInManager.UserManager.FindByNameAsync(emailOrUserName)
                ?? throw new InfrastructureException("Could not find user with given email or username", "UserNotFound");

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

            return signInResult.ToInfraTaskResult([new IdentityError { Code = "InvalidCredentials", Description = "Password does not match the given user." }]);
        }
    }
}
