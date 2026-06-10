using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Auth.Authorization
{
    public interface IAuthorizationService
    {
        public Task<ITaskResult<Domain.Models.User.User>> GetOwnerAsync(string ownerRole, CancellationToken cancellationToken = default);
        public Task<ITaskResult<IEnumerable<Domain.Models.User.User>>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default);
        public Task<IEnumerable<string>?> GetUserRolesAsync(string userId, CancellationToken cancellationToken = default);
        public Task<IEnumerable<string>?> GetUserPermissionsAsync(string userId, CancellationToken cancellationToken = default);
        public Task<ITaskResult<bool>> UserHasPermissionAsync(string userId,string permissionName, CancellationToken cancellationToken = default);
        public Task<ITaskResult> GiveUserPermission(string userId, string permissionName, CancellationToken cancellationToken = default);
        public Task<ITaskResult> RevokeUserPermission(string userId, string permissionName, CancellationToken cancellationToken = default);
        public Task<ITaskResult> AddUserToRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default);
        public Task<ITaskResult> RemoveUserFromRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default);
    }
}
