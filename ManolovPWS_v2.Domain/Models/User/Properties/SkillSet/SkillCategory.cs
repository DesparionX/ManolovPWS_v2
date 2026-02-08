using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.SkillSet
{
    public sealed class SkillCategory : IEquatable<SkillCategory>
    {
        public string Name { get; }

        [JsonConstructor]
        private SkillCategory(string name)
        {
            Name = name;
        }

        public static SkillCategory Create(string name)
        {
            name = NormalizeSkillCategoryName(name);
            ValidateSkillCategory(name);

            return new SkillCategory(name);
        }

        private static string NormalizeSkillCategoryName(string name) =>
            name.Trim().ToLowerInvariant();

        // Validation
        private static void ValidateSkillCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidSkillException("Skill category name cannot be null or empty.", "InvalidSkillCategory");
        }

        // Equality
        public bool Equals(SkillCategory? other) =>
            other is not null &&
            StringComparer.OrdinalIgnoreCase.Equals(Name, other.Name);
        public override bool Equals(object? obj) => Equals(obj as SkillCategory);
        public override int GetHashCode() => Name.GetHashCode();
    }
}
