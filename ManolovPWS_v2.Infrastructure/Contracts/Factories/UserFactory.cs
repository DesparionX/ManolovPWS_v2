using ManolovPWS_v2.Domain.Contracts.Factories;
using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Infrastructure.Contracts.Maps;
using ManolovPWS_v2.Infrastructure.Contracts.Results;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using ManolovPWS_v2.Shared.Abstractions.Results;
using Microsoft.AspNetCore.Identity;

namespace ManolovPWS_v2.Infrastructure.Contracts.Factories
{
    public sealed class UserFactory(UserManager<DbUser> userManager) : IUserFactory
    {
        private readonly UserManager<DbUser> _userManager = userManager;

        public async Task<ITaskResult<User>> CreateAsync(User entity, CancellationToken cancellationToken = default)
        {
            var dbUser = entity.ToDbEntity();

            var result = await _userManager.CreateAsync(dbUser);

            if (!result.Succeeded)
                return Result<User>.Failure(result.Errors.Select(e => new InfraError(e.Code, e.Description)).ToList());

            return Result<User>.Success(dbUser.ToDomain());
        }

        public async Task<ITaskResult<User>> CreateWithPasswordAsync(
            User entity,
            string password,
            CancellationToken cancellationToken = default)
        {
            var dbUser = entity.ToDbEntity();

            var result = await _userManager.CreateAsync(dbUser, password);

            if (!result.Succeeded)
                return Result<User>.Failure(result.Errors.Select(e => new InfraError(e.Code, e.Description)).ToList());

            return Result<User>.Success(dbUser.ToDomain());
        }
    }
}
