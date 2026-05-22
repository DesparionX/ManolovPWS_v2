using ManolovPWS_v2.Api.Contracts.Identity;
using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Modules.Identity.User.Features.RegisterUser;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

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
    }
}
