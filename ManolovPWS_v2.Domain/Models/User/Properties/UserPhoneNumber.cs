using ManolovPWS_v2.Domain.Models.User.Exceptions;
using PhoneNumbers;

namespace ManolovPWS_v2.Domain.Models.User.Properties
{
    public sealed class UserPhoneNumber : IEquatable<UserPhoneNumber>
    {
        public string Value { get; }

        private UserPhoneNumber(string value)
        {
            Value = value;
        }

        public static UserPhoneNumber? CreateOrNull(string? rawNumber, string region = "BG")
        {
            if (string.IsNullOrEmpty(rawNumber)) return null;

            var normalized = ValidateNumber(rawNumber, region);

            return new(normalized);
        }

        // Validations
        public static string ValidateNumber(string rawNumber, string region)
        {
            var util = PhoneNumberUtil.GetInstance();

            try
            {
                var parsed = util.Parse(rawNumber, region);

                if (!util.IsValidNumber(parsed))
                    throw new InvalidPhoneNumberException("Invalid phone number.");
                
                var normalized = util.Format(parsed, PhoneNumberFormat.E164);

                return normalized;
            }
            catch (NumberParseException)
            {
                throw new InvalidPhoneNumberException("Invalid phone number.");
            }
        }

        public bool Equals(UserPhoneNumber? other) =>
            other is not null
            && StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj)
            => Equals(obj as UserPhoneNumber);

        public override int GetHashCode() =>
            Value.GetHashCode();
    }
}
