using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Modules.Content.CV.Features;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CVController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet]
        public async Task<IActionResult> LoadCv()
        {
            var query = new GetUserCVQuery();
            var result = await _dispatcher.QueryAsync(query);
            return result.ToActionResult();
        }
    }
}
