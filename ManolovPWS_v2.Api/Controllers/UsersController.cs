using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController(IDispatcher dispatcher, IUserRepository userRepository) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;
        private readonly IUserRepository _userRepository = userRepository;

        // This is a temporary endpoint for testing purposes. It will be removed in the future.
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userRepository.GetAllAsync();
            return result.ToActionResult<IEnumerable<User>>();
        }
    }
}
