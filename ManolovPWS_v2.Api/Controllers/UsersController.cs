using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.User.Features.GetUser;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController(IDispatcher dispatcher, ICurrentUser<UserId> currentUser) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        [Authorize(Roles = Roles.Owner)]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken = default)
        {
            var q = new GetAllUsersQuery();
            var result = await _dispatcher.QueryAsync(q, cancellationToken);

            return result.ToActionResult();
        }

        [AllowAnonymous]
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(string id, CancellationToken cancellationToken = default)
        {
            var isOwner = _currentUser.IsAuthenticated && _currentUser.IsInRole(Roles.Owner);

            if (isOwner)
            {
                var privateQ = new GetUserPrivateProfileQuery(id);
                var privateResult = await _dispatcher.QueryAsync(privateQ, cancellationToken);
                return privateResult.ToActionResult();
            }

            var publicQ = new GetUserPublicProfileQuery(id);
            var publicResult = await _dispatcher.QueryAsync(publicQ, cancellationToken);
            return publicResult.ToActionResult();
        }
    }
}

