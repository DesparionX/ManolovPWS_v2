using ManolovPWS_v2.Domain.Models.Message.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Message.Properties.MessageData
{
    public sealed class MessageTitle : IEquatable<MessageTitle>
    {
        public string Value { get; }

        [JsonConstructor]
        private MessageTitle(string value)
        {
            Value = value;
        }

        public static MessageTitle Create(string value)
        {
            ValidateMessageTitle(value);
            return new(value);
        }

        // Validations
        private static void ValidateMessageTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidMessageDataException("Message title cannot be null or empty.", "InvalidMessageTitle");

            if (value.Length is < 3 or > 50)
                throw new InvalidMessageDataException("Message title must be between 3 and 50 characters.", "InvalidMessageTitle");
        }

        // Equality
        public bool Equals(MessageTitle? other) =>
            other is not null &&
            StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) =>
            Equals(obj as MessageTitle);

        public override int GetHashCode() => Value.GetHashCode();
    }
}
