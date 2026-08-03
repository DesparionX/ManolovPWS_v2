using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Domain.Models.Message.Properties;
using ManolovPWS_v2.Domain.Models.Message.Properties.MessageData;
using ManolovPWS_v2.Domain.Models.Message.Properties.SenderInfo;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Message
{
    public sealed class Message : IEntity<MessageId>
    {
        public MessageId Id { get; }
        public SenderInfo Sender { get; }
        public MessageData Data { get; }
        public MessageSentDate SentDate { get; }
        public bool IsUnread { get; private set; }

        [JsonConstructor]
        private Message(
            MessageId id,
            SenderInfo sender,
            MessageData data,
            MessageSentDate sentDate,
            bool isUnread
            )
        {
            Id = id;
            Sender = sender;
            Data = data;
            SentDate = sentDate;
            IsUnread = isUnread;
        }
        private Message AsRead() => new(
            id: this.Id,
            sender: this.Sender,
            data: this.Data,
            sentDate: this.SentDate,
            isUnread: false
            );

        public static Message Create(
            MessageId id,
            SenderInfo sender,
            MessageData data,
            MessageSentDate sentDate,
            bool? isUnread = true
            )
        {
            return new(
                id: id,
                sender: sender,
                data: data,
                sentDate: sentDate,
                isUnread: isUnread ?? true
                );
        }

        // Message manipulations
        public Message MarkAsRead()
            => IsUnread ? AsRead() : this;
    }
}
