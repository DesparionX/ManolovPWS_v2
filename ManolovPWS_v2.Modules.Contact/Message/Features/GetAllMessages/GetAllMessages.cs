using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Modules.Contact.Message.Maps;
using ManolovPWS_v2.Modules.Contact.Message.Shared.ReadModels;
using ManolovPWS_v2.Modules.Contact.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Contact.Message.Features.GetAllMessages
{
    public sealed record GetAllMessages : IQuery<IReadOnlyList<MessageReadModel>>;

    public sealed class GetAllMessagesHandler(IMessageRepository messageRepository)
        : IQueryHandler<GetAllMessages, IReadOnlyList<MessageReadModel>>
    {
        private readonly IMessageRepository _messageRepository = messageRepository;

        public async Task<ITaskResult<IReadOnlyList<MessageReadModel>>> HandleAsync(GetAllMessages query, CancellationToken cancellationToken = default)
        {
            var messagesResult = await _messageRepository.GetAllAsync(cancellationToken);
            if (!messagesResult.IsSuccess)
                return Result<IReadOnlyList<MessageReadModel>>.Failure([ContactAppErrors.NoMessagesFound, .. messagesResult.Errors]);

            var messages = messagesResult.Value;
            var messageReadModels = messages.Select(m => m.ToMessageReadModel()).ToList();

            return Result<IReadOnlyList<MessageReadModel>>.Success(messageReadModels);
        }
    }
}
