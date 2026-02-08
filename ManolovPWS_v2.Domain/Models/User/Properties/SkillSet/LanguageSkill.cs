using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.SkillSet
{
    public sealed class LanguageSkill : IEquatable<LanguageSkill>
    {
        public LanguageName Language { get; }
        public LanguageLevel? Level { get; }
        public bool IsNative => Level is null;

        [JsonConstructor]
        private LanguageSkill(LanguageName language, LanguageLevel? level)
        {
            Language = language;
            Level = level;
        }

        public static LanguageSkill CreateNative(LanguageName language) =>
            new(language, null);

        public static LanguageSkill CreateNonNative(LanguageName language, LanguageLevel level) => 
            new(language, level);

        // Equality
        public bool Equals(LanguageSkill? other) =>
            other is not null &&
            StringComparer.OrdinalIgnoreCase.Equals(Language.Value, other.Language.Value);
        public override bool Equals(object? obj) => Equals(obj as LanguageSkill);
        public override int GetHashCode() => Language.GetHashCode();
        public override string ToString() => Language.Value.ToString();
    }
}
