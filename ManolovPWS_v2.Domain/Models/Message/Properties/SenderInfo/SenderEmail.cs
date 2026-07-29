using ManolovPWS_v2.Domain.Models.Message.Exceptions;
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Message.Properties.SenderInfo
{
    public sealed class SenderEmail : IEquatable<SenderEmail>
    {
        public string Value { get; }

        [JsonConstructor]
        private SenderEmail(string value)
        {
            Value = value;
        }

        public static SenderEmail Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidMessageSenderException("Email cannot be null or empty.", "NullOrEmptyEmail");

            try
            {
                var mail = new MailAddress(value);
                return new SenderEmail(mail.Address.ToLowerInvariant());
            }
            catch
            {
                throw new InvalidMessageSenderException("Email format is invalid.", "InvalidEmailFormat");
            }
        }

        public bool Equals(SenderEmail? other) =>
            other is not null &&
            StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as SenderEmail);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
