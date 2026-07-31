using ManolovPWS_v2.Api.Maps;
using ManolovPWS_v2.Api.Services;
using ManolovPWS_v2.Modules.Contact.Message.Features.DeleteMessage;
using ManolovPWS_v2.Modules.Contact.Message.Features.GetAllMessages;
using ManolovPWS_v2.Modules.Contact.Message.Features.ReadMessage;
using ManolovPWS_v2.Modules.Contact.Message.Shared.ReadModels;
using ManolovPWS_v2.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManolovPWS_v2.Api.Controllers
{
    [Authorize(Roles = Roles.Owner)]
    [Route("[controller]")]
    [ApiController]
    public class InboxController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpGet("messages")]
        [ProducesResponseType<IReadOnlyList<MessageReadModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMessages(CancellationToken cancellationToken = default)
        {
            var query = new GetAllMessagesQuery();
            var result = await _dispatcher.QueryAsync(query, cancellationToken);

            return result.ToActionResult();
        }

        [HttpPut("messages/{id}")]
        [ProducesResponseType<MessageReadModel>(StatusCodes.Status200OK)]
        public async Task<IActionResult> ReadMessage(string id, CancellationToken cancellationToken = default)
        {
            var cmd = new ReadMessageCommand(MessageId: id);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }

        [HttpDelete("messages/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteMessage(string id, CancellationToken cancellationToken = default)
        {
            var cmd = new DeleteMessageCommand(MessageId: id);
            var result = await _dispatcher.SendAsync(cmd, cancellationToken);
            return result.ToActionResult();
        }
    }
}
