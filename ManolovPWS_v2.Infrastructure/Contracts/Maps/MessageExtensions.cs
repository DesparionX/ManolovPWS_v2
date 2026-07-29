using ManolovPWS_v2.Domain.Models.Message;
using ManolovPWS_v2.Domain.Models.Message.Properties;
using ManolovPWS_v2.Domain.Models.Message.Properties.MessageData;
using ManolovPWS_v2.Domain.Models.Message.Properties.SenderInfo;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;

namespace ManolovPWS_v2.Infrastructure.Contracts.Maps
{
    public static class MessageExtensions
    {
        public static Message ToDomain(this DbMessage message) =>
            Message.Create(
                id: MessageId.From(message.Id.ToString()),
                sender: SenderInfo.Create(
                    metadata: message.SenderMetadata,
                    username: SenderUsername.Create(message.SenderName),
                    email: SenderEmail.Create(message.SenderEmail)
                    ),
                data: MessageData.Create(
                    title: MessageTitle.Create(message.Title),
                    context: MessageContext.Create(message.Context)
                    ),
                sentDate: MessageSentDate.Create(message.SentDate),
                isUnread: message.IsUnread
                );

        public static IReadOnlyList<Message> ToDomainList(this IEnumerable<DbMessage> messages) =>
            messages.Select(m => m.ToDomain()).ToList();

        public static DbMessage ToDbEntity(this Message message) =>
            new()
            {
                Id = message.Id.Value,
                SenderMetadata = message.Sender.Metadata,
                SenderName = message.Sender.Username.Value,
                SenderEmail = message.Sender.Email.Value,
                Title = message.Data.Title.Value,
                Context = message.Data.Context.Value,
                SentDate = message.SentDate.Value,
                IsUnread = message.IsUnread
            };

        public static IReadOnlyList<DbMessage> ToDbEntityList(this IEnumerable<Message> messages) =>
            messages.Select(m => m.ToDbEntity()).ToList();

        public static void ApplyChanges(this DbMessage dbMessage, Message message)
        {
            dbMessage.SenderMetadata = message.Sender.Metadata;
            dbMessage.SenderName = message.Sender.Username.Value;
            dbMessage.SenderEmail = message.Sender.Email.Value;
            dbMessage.Title = message.Data.Title.Value;
            dbMessage.Context = message.Data.Context.Value;
            dbMessage.SentDate = message.SentDate.Value;
            dbMessage.IsUnread = message.IsUnread;
        }
    }
}
