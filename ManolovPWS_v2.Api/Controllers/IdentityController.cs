using ManolovPWS_v2.Api.Contracts.Identity;
using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.User.Features.ManageTokens;
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
            if (!result.IsSuccess)
                return result.ToActionResult();

            Response.Cookies.Append(
                "refreshToken",
                result.Value.RefreshToken.Token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = result.Value.RefreshToken.ExpiresAtUtc,
                    Path = "/Identity"
                }
             );

            var response = new
            {
                result.Value.AccessToken,
                AuthUser = result.Value.User
            };

            return Ok(response);
        }

        [Authorize]
        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            if (_currentUser.IsAuthenticated)
                return Ok(_currentUser);

            return Unauthorized();
        }

        [Authorize]
        [HttpPost("sign-out")]
        public async Task<IActionResult> SignOutUser(CancellationToken cancellationToken = default)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized();

            var cmd = new SignOutCommand(refreshToken);

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);

            if (!result.IsSuccess)
                return result.ToActionResult();

            Response.Cookies.Delete(
                "refreshToken",
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Path = "/Identity"
                });

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken = default)
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized();

            var cmd = new RefreshAccessTokenCommand(refreshToken);

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);

            if (!result.IsSuccess)
                return result.ToActionResult();

            Response.Cookies.Append(
                "refreshToken",
                result.Value.RefreshToken.Token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = result.Value.RefreshToken.ExpiresAtUtc,
                    Path = "/Identity"
                }
             );

            var response = new
            {
                result.Value.AccessToken
            };

            return Ok(response);
        }
    }
}
