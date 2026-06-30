using ManolovPWS_v2.Api.Contracts.Admin;
using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Modules.Identity.User.Features.Admin;
using ManolovPWS_v2.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController(
        IDispatcher dispatcher
        ) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        // Roles endpoints

        [Authorize(Roles = Roles.Owner)]
        [HttpGet("users/{id}/roles")]
        public async Task<IActionResult> GetUserRoles(string id, CancellationToken cancellationToken = default)
        {
            var query = new GetUserRolesQuery(
                UserId: id
                );

            var result = await _dispatcher.QueryAsync(query, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpPost("users/{id}/roles")]
        public async Task<IActionResult> AddUserToRole(string id, [FromBody] UserRoleRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new AddUserToRoleCommand(
                UserId: id,
                Role: request.Role
                );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpDelete("users/{id}/roles")]
        public async Task<IActionResult> RemoveUserFromRole(string id, [FromBody] UserRoleRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new RemoveUserFromRoleCommand(
                UserId: id,
                Role: request.Role
                );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }


        // Permissions endpoints

        [Authorize(Roles = Roles.Owner)]
        [HttpGet("users/{id}/permissions")]
        public async Task<IActionResult> GetUserPermissions(string id, CancellationToken cancellationToken = default)
        {
            var query = new GetUserPermissionsQuery(
                UserId: id
                );

            var result = await _dispatcher.QueryAsync(query, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpPost("users/{id}/permissions")]
        public async Task<IActionResult> GiveUserPermission(string id, [FromBody] UserPermissionRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new GiveUserPermissionCommand(
                UserId: id,
                Permission: request.Permission
                );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [Authorize(Roles = Roles.Owner)]
        [HttpDelete("users/{id}/permissions")]
        public async Task<IActionResult> RevokeUserPermission(string id, [FromBody] UserPermissionRequest request, CancellationToken cancellationToken = default)
        {
            var cmd = new RevokeUserPermissionCommand(
                UserId: id,
                Permission: request.Permission
                );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }
    }
}
