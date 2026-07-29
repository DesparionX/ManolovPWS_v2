using ManolovPWS_v2.Domain.Models.Message.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Message.Properties.SenderInfo
{
    public sealed class SenderUsername : IEquatable<SenderUsername>
    {
        public string Value { get; }

        [JsonConstructor]
        private SenderUsername(string value)
        {
            Value = value;
        }

        public static SenderUsername Create(string value)
        {
            ValidateUsername(value);
            return new(value);
        }

        // Validations
        private static void ValidateUsername(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidMessageSenderException("Sender username cannot be null or whitespace.", "InvalidSenderUsername");
        
            if (value.Length is < 2 or > 30)
                throw new InvalidMessageSenderException("Sender username must be between 2 and 30 characters.", "InvalidSenderUsername");
        }

        // Equality
        public bool Equals(SenderUsername? other) =>
            other is not null &&
            StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) =>
            Equals(obj as SenderUsername);
        
        public override int GetHashCode() => 
            Value.GetHashCode();
    }
}
