using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Infrastructure.Contracts.Maps;
using ManolovPWS_v2.Infrastructure.Contracts.Results;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
using ManolovPWS_v2.Shared.Abstractions.Results;
using ManolovPWS_v2.Shared.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ManolovPWS_v2.Infrastructure.Contracts.Authorization
{
    public sealed class AuthorizationService(UserManager<DbUser> userManager)
        : IAuthorizationService
    {
        private readonly UserManager<DbUser> _userManager = userManager;

        public async Task<ITaskResult<User>> GetOwnerAsync(string ownerRole, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!RoleExists(ownerRole))
                return Result<User>.Failure([new InfraError(Code: "RoleNotFound", Message: $"Role '{ownerRole}' does not exist.")]);

            var usersInRole = await _userManager.GetUsersInRoleAsync(ownerRole);

            var owner = usersInRole.FirstOrDefault();
            if (owner is null)
                return Result<User>.Failure([new InfraError(Code: "OwnerNotFound", Message: $"No user found in the '{ownerRole}' role.")]);

            return Result<User>.Success(owner.ToDomain());
        }
        public async Task<ITaskResult<IEnumerable<User>>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

            return Result<IEnumerable<User>>.Success(usersInRole.ToList().ToDomainList());
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
        public async Task<ITaskResult> GiveUserPermissionAsync(string userId, string permissionName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure([new InfraError(Code: "UserNotFound", Message: $"User with ID '{userId}' not found.")]);

            if (!PermissionExists(permissionName))
                return Result.Failure([new InfraError(Code: "PermissionNotFound", Message: $"Permission '{permissionName}' does not exist.")]);

            if (await HasPermissionAsync(user, permissionName))
                return Result.Failure([new InfraError(Code: "PermissionAlreadyAssigned", Message: $"User already has the '{permissionName}' permission.")]);

            var claim = new Claim(CustomClaimTypes.Permission, permissionName);

            var result = await _userManager.AddClaimAsync(user, claim);

            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => new InfraError(e.Code, e.Description)).ToList());
        }
        public async Task<ITaskResult> RevokeUserPermissionAsync(string userId, string permissionName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure([new InfraError(Code: "UserNotFound", Message: $"User with ID '{userId}' not found.")]);

            if (!PermissionExists(permissionName))
                return Result.Failure([new InfraError(Code: "PermissionNotFound", Message: $"Permission '{permissionName}' does not exist.")]);

            if (!await HasPermissionAsync(user, permissionName))
                return Result.Failure([new InfraError(Code: "PermissionNotAssigned", Message: $"User does not have the '{permissionName}' permission.")]);

            var claim = new Claim(CustomClaimTypes.Permission, permissionName);

            var result = await _userManager.RemoveClaimAsync(user, claim);

            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => new InfraError(e.Code, e.Description)).ToList());
        }
        public async Task<ITaskResult> AddUserToRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Result.Failure([new InfraError(Code: "UserNotFound", Message: $"User with ID '{userId}' not found.")]);

            if (!RoleExists(roleName))
                return Result.Failure([new InfraError(Code: "RoleNotFound", Message: $"Role '{roleName}' does not exist.")]);

            if (await _userManager.IsInRoleAsync(user, roleName))
                return Result.Failure([new InfraError(Code: "UserAlreadyInRole", Message: $"User is already in the '{roleName}' role.")]);

            var result = await _userManager.AddToRoleAsync(user, roleName);

            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => new InfraError(e.Code, e.Description)).ToList());
        }
        public async Task<ITaskResult> RemoveUserFromRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure([new InfraError(Code: "UserNotFound", Message: $"User with ID '{userId}' not found.")]);

            if (!RoleExists(roleName))
                return Result.Failure([new InfraError(Code: "RoleNotFound", Message: $"Role '{roleName}' does not exist.")]);

            if (!await _userManager.IsInRoleAsync(user, roleName))
                return Result.Failure([new InfraError(Code: "UserNotInRole", Message: $"User is not in the '{roleName}' role.")]);

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => new InfraError(e.Code, e.Description)).ToList());
        }

        // Helper methods
        private static bool PermissionExists(string permissionName)
            => Permissions.AllPermissions.Any(p => p.Equals(permissionName, StringComparison.OrdinalIgnoreCase));
        private async Task<bool> HasPermissionAsync(DbUser user, string permissionName)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            if (claims is null) return false;
            
            return claims.Any(c => c.Type.Equals(CustomClaimTypes.Permission) && c.Value.Equals(permissionName));
        }
        private static bool RoleExists(string roleName)
            => Roles.AllRoles.Any(r => r.Equals(roleName, StringComparison.OrdinalIgnoreCase));

    }
}
