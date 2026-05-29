using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Authorization;
using System.Security.Claims;

namespace ManolovPWS_v2.Api.Contracts.Identity
{
    public sealed class CurrentUser(IHttpContextAccessor httpContextAccessor)
        : ICurrentUser<UserId>
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private ClaimsPrincipal? Principal =>
            _httpContextAccessor.HttpContext?.User;

        public UserId Id
        {
            get
            {
                var id = Principal?.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrWhiteSpace(id))
                    return UserId.From(id);

                throw new InvalidOperationException("User is not authenticated.");
            }
        }
        public string UserName
        {
            get
            {
                var userName = 
                    Principal?.FindFirstValue(ClaimTypes.Name)
                    ?? Principal?.FindFirstValue("unique_name");

                if (!string.IsNullOrWhiteSpace(userName))
                    return userName;

                throw new InvalidOperationException("User is not authenticated.");
            }
        }
        public string Email
        {
            get
            {
                var email = 
                    Principal?.FindFirstValue(ClaimTypes.Email)
                    ?? Principal?.FindFirstValue("email");

                if (!string.IsNullOrWhiteSpace(email))
                    return email;

                throw new InvalidOperationException("User is not authenticated.");
            }
        }
        public bool IsInRole(string role) =>
            Principal?.IsInRole(role) ?? false;
        public bool HasPermission(string permission) =>
            Principal?.HasClaim(CustomClaimTypes.Permission, permission) ?? false;
        public bool IsAuthenticated =>
            Principal?.Identity?.IsAuthenticated ?? false;

        public IReadOnlyCollection<string> GetRoles() =>
            Principal?
            .FindAll(ClaimTypes.Role)
            .Select(c => c.Value)
            .ToArray() ?? [];
        public IReadOnlyCollection<string> GetPermissions() =>
            Principal?
            .FindAll(CustomClaimTypes.Permission)
            .Select(c => c.Value)
            .ToArray() ?? [];
    }
}
