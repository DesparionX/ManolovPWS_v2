using ManolovPWS_v2.Domain.Models.Message.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Message.Properties
{
    public sealed class MessageSentDate : IEquatable<MessageSentDate>
    {
        public DateTime Value { get; }

        [JsonConstructor]
        private MessageSentDate(DateTime value)
        {
            Value = value;
        }

        public static MessageSentDate Create(DateTime value)
        {
            ValidateDate(value);
            return new(value);
        }

        // Validations
        private static void ValidateDate(DateTime value)
        {
            if (value > DateTime.UtcNow)
                throw new InvalidMessageSentDateException("Message sent date cannot be in the future.", "InvalidMessageSentDate");
        }

        // Equality
        public bool Equals(MessageSentDate? other) =>
            other is not null
            && DateTime.Equals(Value, other.Value);

        public override bool Equals(object? obj) =>
            Equals(obj as MessageSentDate);

        public override int GetHashCode() =>
            Value.GetHashCode();
    }
}
