using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.SkillSet
{
    public sealed class Skill : IEquatable<Skill>
    {
        public SkillName Name { get; }
        public SkillLevel Level { get; }
        public SkillType Type { get; }
        public SkillCategory Category { get; }

        [JsonConstructor]
        private Skill(SkillName name, SkillLevel level, SkillType type, SkillCategory category)
        {
            Name = name;
            Level = level;
            Type = type;
            Category = category;
        }

        public static Skill Create(SkillName name, SkillType type, SkillCategory category, SkillLevel? level = default)
        {
            level ??= SkillLevel.Create(0);

            ValidateSkill(name, level, type, category);

            return new Skill(name, level, type, category);
        }

        // Validations
        private static void ValidateSkill(SkillName name, SkillLevel level, SkillType type, SkillCategory category)
        {
            if (name is null || string.IsNullOrWhiteSpace(name.Value))
                throw new InvalidSkillException("Skill name cannot be null.", "InvalidSkillName");

            if (level is null)
                throw new InvalidSkillException("Skill level cannot be null.", "InvalidSkillLevel");

            if (!IsValidSkillType(type))
                throw new InvalidSkillException("Skill type is invalid.", "InvalidSkillType");

            if (category is null || string.IsNullOrWhiteSpace(category.Name))
                throw new InvalidSkillException("Skill category cannot be null.", "InvalidSkillCategory");
        }

        private static bool IsValidSkillType(SkillType type) =>
            type >= SkillType.Tech && type <= SkillType.Soft;

        // Equality
        public bool Equals(Skill? other) =>
            other is not null &&
            StringComparer.OrdinalIgnoreCase.Equals(Name, other.Name);

        public override bool Equals(object? obj) => Equals(obj as Skill);
        public override int GetHashCode() => Name.GetHashCode();
        public override string ToString() => $"{Name.Value} (Level: {Level.Value})";
    }
}
