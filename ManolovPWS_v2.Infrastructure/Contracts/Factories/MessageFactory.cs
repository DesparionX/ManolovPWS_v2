using ManolovPWS_v2.Domain.Contracts.Factories;
using ManolovPWS_v2.Domain.Models.Message;
using ManolovPWS_v2.Infrastructure.Contracts.Maps;
using ManolovPWS_v2.Infrastructure.Contracts.Results;
using ManolovPWS_v2.Infrastructure.Persistance;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Infrastructure.Contracts.Factories
{
    public sealed class MessageFactory(AppDbContext context) : IMessageFactory
    {
        private readonly AppDbContext _context = context;

        public async Task<ITaskResult<Message>> CreateAsync(Message message, CancellationToken cancellationToken = default)
        {
            var dbMessage = message.ToDbEntity();

            _context.Messages.Add(dbMessage);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ?
                Result<Message>.Success(dbMessage.ToDomain())
                : Result<Message>.Failure([new InfraError(Code: "MessageCreationFailed", Message: "Failed to create the message.")]);
        }
    }
}
