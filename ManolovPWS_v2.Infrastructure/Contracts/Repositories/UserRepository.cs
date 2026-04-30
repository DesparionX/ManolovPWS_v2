using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Infrastructure.Contracts.Maps;
using ManolovPWS_v2.Infrastructure.Contracts.Results;
using ManolovPWS_v2.Infrastructure.Exceptions;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using ManolovPWS_v2.Shared.Abstractions.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ManolovPWS_v2.Infrastructure.Contracts.Repositories
{
    public sealed class UserRepository(UserManager<DbUser> userManager) : IUserRepository
    {
        private readonly UserManager<DbUser> _userManager = userManager;

        public async Task<ITaskResult<IReadOnlyList<User>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userManager.Users.ToListAsync(cancellationToken);

            return Result<IReadOnlyList<User>>.Success(users.ToDomainList());
        }

        public async Task<ITaskResult<User>> FindByEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByEmailAsync(email.Value) ??
                throw DbExceptions.UserNotFound(email.Value);

            return Result<User>.Success(user.ToDomain());
        }

        public async Task<ITaskResult<User>> FindByUserNameAsync(UserName userName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByNameAsync(userName.Value) ??
                throw DbExceptions.UserNotFound(userName.Value);

            return Result<User>.Success(user.ToDomain());
        }

        public async Task<ITaskResult<User>> FindByIdAsync(UserId id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(id.Value.ToString()) ??
                throw DbExceptions.UserNotFound(id.Value.ToString());

            return Result<User>.Success(user.ToDomain());
        }

        public async Task<ITaskResult> RemoveAsync(UserId id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(id.Value.ToString()) ??
                throw DbExceptions.UserNotFound(id.Value.ToString());

            var result = await _userManager.DeleteAsync(user);

            return result.ToInfraTaskResult();
        }

        public async Task<ITaskResult> SaveAsync(User entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _userManager.UpdateAsync(entity.ToDbEntity());

            return result.ToInfraTaskResult();
        }

        public async Task<bool> EmailExistsAsync(Email email, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByEmailAsync(email.Value);

            return user is not null;
        }
        
        public async Task<bool> UserNameExistsAsync(UserName userName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByNameAsync(userName.Value);

            return user is not null;
        }
    }
}
