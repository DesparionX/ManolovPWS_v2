using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.SkillSet
{
    public sealed class SkillLevel : IEquatable<SkillLevel>
    {
        public int Value { get; }

        [JsonConstructor]
        private SkillLevel(int value)
        {
            Value = value;
        }

        public static SkillLevel Create(int value)
        {
            ValidateSkillLevel(value);

            return new SkillLevel(value);
        }

        // Validations
        private static void ValidateSkillLevel(int value)
        {
            if (value < 0 || value > 10)
                throw new InvalidSkillException("Skill level must be between 1 and 10.", "SkillLevelOutOfRange");
        }

        // Equality
        public override bool Equals(object? obj) => Equals(obj as SkillLevel);
        public bool Equals(SkillLevel? other) =>
            other is not null &&
            Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}
