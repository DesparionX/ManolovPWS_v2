using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System.IO.IsolatedStorage;

namespace ManolovPWS_v2.Domain.Models.User.Properties
{
    public sealed class Gender : IEquatable<Gender>
    {
        public GenderType Value { get; }
        public bool IsMale => Value == GenderType.Male;
        public bool IsFemale => Value == GenderType.Female;

        private Gender(GenderType gender)
        {
            Value = gender;
        }

        public static Gender Male => new(GenderType.Male);
        public static Gender Female => new(GenderType.Female);

        public static Gender FromString(string value)
        {
            string normalized = Normalized(value);

            ValidateGender(normalized);

            return normalized.Equals("male") ? Male : Female;
        }

        // Validations
        private static string Normalized(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidGenderException("Gender cannot be null or empty.");

            return value.Trim().ToLowerInvariant();
        }
        private static void ValidateGender(string gender)
        {
            if (gender is not "male" and not "female")
                throw new InvalidGenderException("Invalid gender.");
        }

        // Equality

        public bool Equals(Gender? other)
            => other is not null && Value == other.Value;

        public override bool Equals(object? obj) => Equals(obj as Gender);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
