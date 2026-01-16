using ManolovPWS_v2.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User.Properties
{
    public sealed class Email : IEquatable<Email>
    {
        public string Value { get; }
        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidEmailException("Email cannot be null or empty.");

            try
            {
                var mail = new MailAddress(value);
                return new Email(mail.Address.ToLowerInvariant());
            }
            catch
            {
                throw new InvalidEmailException("Email format is invalid.");
            }
        }

        public bool Equals(Email? other) =>
            other is not null && 
            StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as Email);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
