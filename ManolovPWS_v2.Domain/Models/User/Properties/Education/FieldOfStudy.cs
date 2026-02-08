using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Education
{
    public sealed class FieldOfStudy : IEquatable<FieldOfStudy>
    {
        public string Value { get; }

        [JsonConstructor]
        private FieldOfStudy(string value)
        {
            Value = value;
        }

        public static FieldOfStudy Create(string value)
        {
            ValidateFieldOfStudy(value);

            return new FieldOfStudy(value);
        }

        // Validations
        private static void ValidateFieldOfStudy(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidEducationException("Field of study cannot be null or empty.", "InvalidFieldOfStudy");
        }

        // Equality
        public bool Equals(FieldOfStudy? other) =>
            other is not null
            && StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as FieldOfStudy);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}