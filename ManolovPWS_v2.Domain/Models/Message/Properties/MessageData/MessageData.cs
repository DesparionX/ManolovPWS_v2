using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Message.Properties.MessageData
{
    public sealed class MessageData : IEquatable<MessageData>
    {
        public MessageTitle Title { get; }
        public MessageContext Context { get; }

        [JsonConstructor]
        private MessageData(MessageTitle title, MessageContext context)
        {
            Title = title;
            Context = context;
        }

        public static MessageData Create(MessageTitle title, MessageContext context)
        {
            return new(title, context);
        }

        // Equality
        public bool Equals(MessageData? other) =>
            other is not null &&
            Title.Equals(other.Title) &&
            Context.Equals(other.Context);

        public override bool Equals(object? obj) =>
            Equals(obj as MessageData);

        public override int GetHashCode() => HashCode.Combine(Title, Context);
    }
}
