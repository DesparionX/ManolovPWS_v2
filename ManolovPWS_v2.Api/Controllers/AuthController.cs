using ManolovPWS_v2.Api.Contracts.Identity;
using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Modules.Identity.User.Features.ManageTokens;
using ManolovPWS_v2.Modules.Identity.User.Features.RegisterUser;
using ManolovPWS_v2.Modules.Identity.User.Features.SignInUser;
using ManolovPWS_v2.Modules.Identity.User.Features.SignOutUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [ProducesResponseType<SignInApiResponse>(StatusCodes.Status200OK)]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignInUser([FromBody] SignInRequest request)
        {
            var cmd = new SignInUserCommand(
                UserNameOrEmail: request.UserNameOrEmail,
                Password: request.Password
                );

            var result = await _dispatcher.SendAsync(cmd);
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
                    Path = "/Auth"
                }
             );

            var response = new SignInApiResponse
            (
                AccessToken: result.Value.AccessToken,
                User: result.Value.User
            );

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
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
                    Path = "/Auth"
                });

            return Ok();
        }

        [ProducesResponseType<RefreshTokenResponse>(StatusCodes.Status200OK)]
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
                    Path = "/Auth"
                }
             );

            var response = new RefreshTokenResponse
            (
                AccessToken: result.Value.AccessToken
            );

            return Ok(response);
        }
    }
}
