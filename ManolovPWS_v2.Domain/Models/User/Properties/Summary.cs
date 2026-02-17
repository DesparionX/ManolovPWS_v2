using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties
{
    public sealed class Summary : IEquatable<Summary>
    {
        public string Value { get; }

        [JsonConstructor]
        private Summary(string value)
        {
            Value = value;
        }

        public static Summary Create(string value)
        {
            Validate(value);

            return new(value);
        }

        public static Summary? CreateOrNull(string? value)
            => string.IsNullOrWhiteSpace(value) ? null : Create(value);

        // Validations
        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidSummaryException("Summary cannot be empty.");

            if (value.Length > 5000)
                throw new InvalidSummaryException("Summary cannot exceed 1000 characters.");
        }

        // Equality
        public bool Equals(Summary? other) =>
            other is not null
            && StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as Summary);

        public override int GetHashCode() => 
            StringComparer.OrdinalIgnoreCase.GetHashCode(Value);

        public override string ToString() => Value;
    }
}
