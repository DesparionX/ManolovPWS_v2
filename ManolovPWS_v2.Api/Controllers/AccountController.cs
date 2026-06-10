using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.User.Features.GetUser;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController(ICurrentUser<UserId> currentUser, IDispatcher dispatcher) : ControllerBase
    {
        private readonly ICurrentUser<UserId> _currentUser = currentUser;
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet("me")]
        public async Task<IActionResult> LoadProfile()
        {
            var id = _currentUser.Id.ToString();
            var q = new GetUserPrivateProfileQuery(id);
            var result = await _dispatcher.QueryAsync(q);

            return result.ToActionResult();
        }
    }
}

