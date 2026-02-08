using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Education
{
    public sealed class Degree : IEquatable<Degree>
    {
        public string Value { get; }

        [JsonConstructor]
        private Degree(string value)
        {
            Value = value; 
        }

        public static Degree Create(string value)
        {
            ValidateDegree(value);

            return new Degree(value);
        }

        // Validations
        private static void ValidateDegree(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidEducationException("Degree cannot be null or empty.", "InvalidDegree");
        }

        // Equality
        public bool Equals(Degree? other) =>
            other is not null
            && StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as Degree);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}