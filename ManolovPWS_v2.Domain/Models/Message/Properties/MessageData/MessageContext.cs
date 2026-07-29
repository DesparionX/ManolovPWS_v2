using ManolovPWS_v2.Domain.Models.Message.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Message.Properties.MessageData
{
    public sealed class MessageContext : IEquatable<MessageContext>
    {
        public string Value { get; }

        [JsonConstructor]
        private MessageContext(string value)
        {
            Value = value;
        }

        public static MessageContext Create(string value)
        {
            ValidateMessageContext(value);

            return new(value);
        }

        // Validation

        private static void ValidateMessageContext(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidMessageDataException("Message context cannot be null or empty.", "InvalidMessageContext");

            if (value.Length is < 3 or > 10000)
                throw new InvalidMessageDataException("Message context must be between 3 and 10000 characters long.", "InvalidMessageContext");

        }

        // Equality
        public bool Equals(MessageContext? other) => 
            other is not null &&
            StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) =>
            Equals(obj as MessageContext);

        public override int GetHashCode() => Value.GetHashCode();
    }
}
