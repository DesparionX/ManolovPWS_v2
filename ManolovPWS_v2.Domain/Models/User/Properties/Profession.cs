using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties
{
    public sealed class Profession : IEquatable<Profession>
    {
        public string Value { get; }

        [JsonConstructor]
        private Profession(string value)
        {
            Value = value;
        }

        public static Profession Create(string value)
        {
            Validate(value);

            return new(value);
        }

        // Validations
        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidProfessionException("Profession cannot be null or empty.");

            if (value.Length > 30)
                throw new InvalidProfessionException("Profession cannot exceed 30 characters.");
        }

        // Equality
        public bool Equals(Profession? other) =>
            other is not null
            && StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as Profession);

        public override int GetHashCode() =>
            StringComparer.OrdinalIgnoreCase.GetHashCode(Value);

        public override string ToString() => Value;
    }
}
