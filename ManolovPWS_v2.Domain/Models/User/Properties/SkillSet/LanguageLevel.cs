using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System.Reflection.Emit;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.SkillSet
{
    public sealed class LanguageLevel : IEquatable<LanguageLevel>
    {
        public CerfLevel Reading { get; }
        public CerfLevel Writing { get; }
        public CerfLevel Speaking { get; }

        [JsonConstructor]
        private LanguageLevel(CerfLevel reding, CerfLevel writing, CerfLevel speaking)
        {
            Reading = reding;
            Writing = writing;
            Speaking = speaking;
        }

        public static LanguageLevel Create(CerfLevel reading, CerfLevel writing, CerfLevel speaking)
        {
            Validate(reading, writing, speaking);
            return new LanguageLevel(reading, writing, speaking);
        }

        // Validations
        private static void Validate(CerfLevel reading, CerfLevel writing, CerfLevel speaking)
        {
            if (!IsValidLevel(reading))
                throw new InvalidSkillException("Reading level is invalid.", "InvalidReadingLevel");

            if (!IsValidLevel(writing))
                throw new InvalidSkillException("Writing level is invalid.", "InvalidWritingLevel");

            if (!IsValidLevel(speaking))
                throw new InvalidSkillException("Speaking level is invalid.", "InvalidSpeakingLevel");
        }

        private static bool IsValidLevel(CerfLevel level) =>
            level >= CerfLevel.A1 && level <= CerfLevel.C2;

        // Equality
        public bool Equals(LanguageLevel? other) =>
            other is not null
            && Reading == other.Reading
            && Writing == other.Writing
            && Speaking == other.Speaking;

        public override bool Equals(object? obj) => Equals(obj as LanguageLevel);
        public override int GetHashCode() => 
            HashCode.Combine(Reading, Writing, Speaking);

        public override string ToString() =>
            $"Reading: {Reading}, Writing: {Writing}, Speaking: {Speaking}";
    }
}