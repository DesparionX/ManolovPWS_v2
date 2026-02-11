using ManolovPWS_v2.Domain.Contracts.Factories;
using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Infrastructure.Contracts.Maps;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using Microsoft.AspNetCore.Identity;

namespace ManolovPWS_v2.Infrastructure.Contracts.Factories
{
    public sealed class UserFactory(UserManager<DbUser> userManager) : IUserFactory
    {
        private readonly UserManager<DbUser> _userManager = userManager;

        public async Task<User?> CreateAsync(User entity, CancellationToken cancellationToken = default)
        {
            var dbUser = entity.ToDbEntity();

            var result = await _userManager.CreateAsync(dbUser);

            if (!result.Succeeded)
                return default;

            return dbUser.ToDomain();
        }

        public async Task<User?> CreateWithPasswordAsync(
            User entity,
            string password,
            CancellationToken cancellationToken = default)
        {
            var dbUser = entity.ToDbEntity();

            var result = await _userManager.CreateAsync(dbUser, password);

            if (!result.Succeeded)
                return default;

            return dbUser.ToDomain();
        }
    }
}
