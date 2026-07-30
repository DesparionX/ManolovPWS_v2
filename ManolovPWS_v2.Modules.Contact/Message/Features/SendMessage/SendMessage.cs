using ManolovPWS_v2.Domain.Contracts.Factories;
using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Message.Properties;
using ManolovPWS_v2.Domain.Models.Message.Properties.MessageData;
using ManolovPWS_v2.Domain.Models.Message.Properties.SenderInfo;
using ManolovPWS_v2.Modules.Contact.Message.Maps;
using ManolovPWS_v2.Modules.Contact.Message.Shared.Properties;
using ManolovPWS_v2.Modules.Contact.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Contact.Message.Features.SendMessage
{
    public sealed record SendMessageCommand(
        string Title,
        string Context,
        string SenderName,
        string SenderEmail,
        SenderMetadataDto SenderMetadata
        ) : ICommand;

    public sealed class SendMessageCommandHandler(IMessageFactory messageFactory, IMessageRepository messageRepository)
        : ICommandHandler<SendMessageCommand>
    {
        private readonly IMessageFactory _messageFactory = messageFactory;
        private readonly IMessageRepository _messageRepository = messageRepository;

        public async Task<ITaskResult> HandleAsync(SendMessageCommand command, CancellationToken cancellationToken = default)
        {
            var metadata = command.SenderMetadata.ToDomainSenderMetadata();
            var waitTime = TimeSpan.FromMinutes(5);

            var sentRecently = await _messageRepository.HasRecentMessageFromAsync(metadata, waitTime, cancellationToken);

            if (sentRecently)
                return Result.Failure([ContactAppErrors.CannotSpam((int)waitTime.TotalMinutes)]);

            var newMessage = Domain.Models.Message.Message.Create(
                id: MessageId.New(),
                sender: SenderInfo.Create(
                    username: SenderUsername.Create(command.SenderName),
                    email: SenderEmail.Create(command.SenderEmail),
                    metadata: metadata
                    ),
                data: MessageData.Create(
                    title: MessageTitle.Create(command.Title),
                    context: MessageContext.Create(command.Context)
                    ),
                sentDate: MessageSentDate.Create(DateTime.UtcNow)
                );

            var result = await _messageFactory.CreateAsync(newMessage, cancellationToken);

            return result.IsSuccess
                ? Result.Success()
                : Result.Failure([ContactAppErrors.FailedToSendMessage]);
        }
    }
}
