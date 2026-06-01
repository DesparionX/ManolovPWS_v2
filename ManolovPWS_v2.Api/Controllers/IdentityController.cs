using ManolovPWS_v2.Api.Contracts.Identity;
using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.User.Features.RegisterUser;
using ManolovPWS_v2.Modules.Identity.User.Features.SignInUser;
using ManolovPWS_v2.Modules.Identity.User.Features.SignOutUser;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController(IDispatcher dispatcher, ICurrentUser<UserId> currentUser) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var command = new RegisterUserCommand(
                UserName: request.UserName,
                Email: request.Email,
                Password: request.Password,
                FirstName: request.FirstName,
                MiddleName: request.MiddleName,
                Profession: request.Profession,
                LastName: request.LastName,
                Gender: request.Gender,
                BirthDate: request.BirthDate
                );

            var result = await _dispatcher.SendAsync(command);

            return result.ToActionResult();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignInUser([FromBody] SignInRequest request)
        {
            var query = new SignInUserCommand(
                UserNameOrEmail: request.UserNameOrEmail,
                Password: request.Password
                );

            var result = await _dispatcher.SendAsync(query);

            return result.ToActionResult();
        }

        [Authorize]
        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            if (_currentUser.IsAuthenticated)
                return Ok(_currentUser);

            return Unauthorized();
        }

        [HttpPost("sign-out")]
        public async Task<IActionResult> SignOutUser()
        {
            var cmd = new SignOutCommand();
            var result = await _dispatcher.SendAsync(cmd);

            return result.ToActionResult();
        }
    }
}
