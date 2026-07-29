using ManolovPWS_v2.Domain.Models.Message.Exceptions;

namespace ManolovPWS_v2.Domain.Models.Message.Properties
{
    public sealed class MessageId : IEquatable<MessageId>
    {
        public Guid Value { get; }
        private MessageId(Guid value)
        {
            Value = value;
        }

        public static MessageId New() => new(Guid.NewGuid());

        public static MessageId From(string value)
        {
            var id = ValidateMessageId(value);
            return new(id);
        }

        // Validations
        private static Guid ValidateMessageId(string value)
        {
            if (!Guid.TryParse(value.ToString(), out Guid id))
                throw new InvalidMessageIdException("This is not a valid GUID.", "InvalidGUID");

            if (id == Guid.Empty)
                throw new InvalidMessageIdException("GUID cannot be null or empty.", "NullOrEmptyGUID");

            return id;
        }

        // Equality
        public bool Equals(MessageId? other) =>
            other is not null
            && Guid.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as MessageId);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
