using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Infrastructure.Contracts.Results;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
using ManolovPWS_v2.Shared.Abstractions.Results;
using ManolovPWS_v2.Shared.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ManolovPWS_v2.Infrastructure.Contracts.Authorization
{
    public sealed class AuthorizationService(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        : IAuthorizationService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;

        public async Task<ITaskResult<IEnumerable<User>>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

            return Result<IEnumerable<User>>.Success(usersInRole);
        }

        public async Task<IEnumerable<string>?> GetUserRolesAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return roles;
        }

        public async Task<IEnumerable<string>?> GetUserPermissionsAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return null;

            var claims = await _userManager.GetClaimsAsync(user);

            var permissions = claims
                .Where(c => c.Type.Equals(CustomClaimTypes.Permission))
                .Select(c => c.Value)
                .ToList();

            return permissions;
        }

        public async Task<ITaskResult<bool>> UserHasPermissionAsync(string userId, string permissionName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result<bool>.Failure([ new InfraError(Code: "UserNotFound", Message: $"User with ID '{userId}' not found.") ]);

            var hasPermission = await _userManager.GetClaimsAsync(user)
                .ContinueWith(claimsTask =>
                {
                    if (claimsTask.IsFaulted || claimsTask.IsCanceled)
                        return false;

                    var claims = claimsTask.Result;

                    return claims.Any(c => c.Type.Equals(CustomClaimTypes.Permission) && c.Value.Equals(permissionName));
                }, cancellationToken);

            return Result<bool>.Success(hasPermission);
        }
    }
}
