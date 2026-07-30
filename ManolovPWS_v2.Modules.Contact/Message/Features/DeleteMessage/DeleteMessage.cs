using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Message.Properties;
using ManolovPWS_v2.Modules.Contact.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Contact.Message.Features.DeleteMessage
{
    public sealed record DeleteMessageCommand(string MessageId) : ICommand;

    public sealed class DeleteMessageCommandHandler(IMessageRepository messageRepository)
        : ICommandHandler<DeleteMessageCommand>
    {
        private readonly IMessageRepository _messageRepository = messageRepository;

        public async Task<ITaskResult> HandleAsync(DeleteMessageCommand command, CancellationToken cancellationToken = default)
        {
            var messageResult = await _messageRepository.FindByIdAsync(MessageId.From(command.MessageId), cancellationToken);

            if (!messageResult.IsSuccess)
                return Result.Failure([ContactAppErrors.MessageNotFound, .. messageResult.Errors]);

            var message = messageResult.Value;

            var deleteResult = await _messageRepository.RemoveAsync(message.Id, cancellationToken);
            if (!deleteResult.IsSuccess)
                return Result.Failure([ContactAppErrors.MessageDeletionFailed, .. deleteResult.Errors]);

            return Result.Success();
        }
    }
}
