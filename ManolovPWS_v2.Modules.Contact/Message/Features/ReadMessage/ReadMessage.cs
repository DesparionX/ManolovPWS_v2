using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Message.Properties;
using ManolovPWS_v2.Modules.Contact.Message.Maps;
using ManolovPWS_v2.Modules.Contact.Message.Shared.ReadModels;
using ManolovPWS_v2.Modules.Contact.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Contact.Message.Features.ReadMessage
{
    public sealed record ReadMessageCommand(string MessageId) : ICommand<MessageReadModel>;

    public sealed class ReadMessageCommandHandler(IMessageRepository messageRepository)
        : ICommandHandler<ReadMessageCommand, MessageReadModel>
    {
        private readonly IMessageRepository _messageRepository = messageRepository;

        public async Task<ITaskResult<MessageReadModel>> HandleAsync(ReadMessageCommand command, CancellationToken cancellationToken = default)
        {
            var messageResult = await _messageRepository.FindByIdAsync(MessageId.From(command.MessageId), cancellationToken);

            if (!messageResult.IsSuccess)
                return Result<MessageReadModel>.Failure([ContactAppErrors.MessageNotFound, ..messageResult.Errors]);

            var message = messageResult.Value;

            if (!message.IsUnread)
                return Result<MessageReadModel>.Success(message.ToMessageReadModel());

            message = message.MarkAsRead();
            var saveResult = await _messageRepository.SaveAsync(message, cancellationToken);

            if (!saveResult.IsSuccess)
                return Result<MessageReadModel>.Failure([ContactAppErrors.FailedToReadMessage]);

            return Result<MessageReadModel>.Success(message.ToMessageReadModel());
        }
    }
}
