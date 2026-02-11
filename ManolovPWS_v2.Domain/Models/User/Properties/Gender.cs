using ManolovPWS_v2.Domain.Models.User.Exceptions;

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
            => new(Parsed(value));

        // Validations
        private static GenderType Parsed(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidGenderException("Gender cannot be null or empty.");

            if (!Enum.TryParse<GenderType>(value, ignoreCase: true, out var parsed)
                || !Enum.IsDefined(parsed))
                throw new InvalidGenderException("Invalid gender.");

            return parsed;
        }

        // Equality
        public bool Equals(Gender? other) => 
            other is not null 
            && Enum.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as Gender);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
