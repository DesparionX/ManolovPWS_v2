using ManolovPWS_v2.Api.Contracts.Inbox;
using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Modules.Contact.Message.Features.SendMessage;
using ManolovPWS_v2.Modules.Contact.Message.Shared.Properties;
using ManolovPWS_v2.Modules.Contact.Message.Shared.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpPost("messages")]
        [ProducesResponseType<MessageReadModel>(StatusCodes.Status201Created)]
        [ProducesResponseType<MessageReadModel>(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateMessage([FromBody] NewMessageRequest request, CancellationToken cancellationToken = default)
        {
            var senderMetadata = new SenderMetadataDto(
                IpAddress: HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                UserAgent: HttpContext.Request.Headers.UserAgent.ToString()
                );

            var cmd = new SendMessageCommand(
                Title: request.Title,
                Context: request.Context,
                SenderName: request.SenderName,
                SenderEmail: request.SenderEmail,
                SenderMetadata: senderMetadata
                );

            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }
    }
}
