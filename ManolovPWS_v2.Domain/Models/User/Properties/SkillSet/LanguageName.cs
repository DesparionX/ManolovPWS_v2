using ManolovPWS_v2.Domain.Models.User.Exceptions;

namespace ManolovPWS_v2.Domain.Models.User.Properties.SkillSet
{
    public sealed class LanguageName : IEquatable<LanguageName>
    {
        public string Value { get; }

        private LanguageName(string value)
        {
            Value = value;
        }

        public static LanguageName Create(string value)
        {
            ValidateName(value);
            return new LanguageName(value.Trim());
        }

        // Validations
        private static void ValidateName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidSkillException("Language name cannot be null or empty.", "InvalidLanguageName");
        }

        // Equality
        public bool Equals(LanguageName? other) =>
            other is not null &&
            StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as LanguageName);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value;
    }
}