using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Infrastructure.Contracts.Maps;
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

        public async Task<ITaskResult<User>> AuthenticateAsync(string emailOrUserName, string password, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _signInManager.UserManager.FindByEmailAsync(emailOrUserName) 
                ?? await _signInManager.UserManager.FindByNameAsync(emailOrUserName)
                ?? throw new InfrastructureException("You have entered an invalid username or password.", "InvalidCredentials");

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

            if (signInResult.Succeeded)
                return Result<User>.Success(user.ToDomain());

            return Result<User>.Failure([new InfraError(Code: "InvalidCredentials", Message: "You have entered an invalid username or password." )]);
        }
    }
}
