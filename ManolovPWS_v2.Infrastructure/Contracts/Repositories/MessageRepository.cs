using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Message;
using ManolovPWS_v2.Domain.Models.Message.Properties;
using ManolovPWS_v2.Domain.Models.Message.Properties.SenderInfo;
using ManolovPWS_v2.Infrastructure.Contracts.Maps;
using ManolovPWS_v2.Infrastructure.Exceptions;
using ManolovPWS_v2.Infrastructure.Persistance;
using ManolovPWS_v2.Shared.Abstractions.Results;
using Microsoft.EntityFrameworkCore;

namespace ManolovPWS_v2.Infrastructure.Contracts.Repositories
{
    public sealed class MessageRepository(AppDbContext context) : IMessageRepository
    {
        private readonly AppDbContext _context = context;
        
        public async Task<ITaskResult<IReadOnlyList<Message>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var messages = await _context.Messages.ToListAsync(cancellationToken);

            return Result<IReadOnlyList<Message>>.Success(messages.ToDomainList());
        }
        public async Task<ITaskResult<Message>> FindByIdAsync(MessageId id, CancellationToken cancellationToken = default)
        {
            var message = await _context.Messages.FindAsync([id.Value], cancellationToken)
                ?? throw DbExceptions.MessageNotFound(id.Value);

            return Result<Message>.Success(message.ToDomain());
        }
        public async Task<ITaskResult> SaveAsync(Message message, CancellationToken cancellationToken = default)
        {
            var dbMessage = await _context.Messages.FindAsync([message.Id.Value], cancellationToken)
                ?? throw DbExceptions.MessageNotFound(message.Id.Value);

            dbMessage.ApplyChanges(message);
            _context.Messages.Update(dbMessage);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<ITaskResult> RemoveAsync(MessageId id, CancellationToken cancellationToken = default)
        {
            var messageToRemove = await _context.Messages.FindAsync([id.Value], cancellationToken)
                ?? throw DbExceptions.MessageNotFound(id.Value);

            _context.Messages.Remove(messageToRemove);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
            await _context.Messages.AnyAsync(cancellationToken);

        public async Task<bool> HasRecentMessageFromAsync(SenderMetadata senderMetadata, TimeSpan timeSpan, CancellationToken cancellationToken = default)
            => await _context.Messages
                .AnyAsync(
                    m => m.SenderMetadata.IpAddress.Equals(senderMetadata.IpAddress) &&
                          m.SenderMetadata.UserAgent.Equals(senderMetadata.UserAgent) &&
                          m.SentDate >= DateTime.UtcNow - timeSpan,
                    cancellationToken
                );
    }
}
