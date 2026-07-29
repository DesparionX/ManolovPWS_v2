using ManolovPWS_v2.Domain.Models.Message;
using ManolovPWS_v2.Domain.Models.Message.Properties;
using ManolovPWS_v2.Domain.Models.Message.Properties.SenderInfo;

namespace ManolovPWS_v2.Domain.Contracts.Repositories
{
    public interface IMessageRepository : IRepository<Message, MessageId>
    {
        public Task<bool> HasRecentMessageFromAsync(SenderMetadata senderMetadata, TimeSpan timeSpan, CancellationToken cancellationToken = default);
    }
}
