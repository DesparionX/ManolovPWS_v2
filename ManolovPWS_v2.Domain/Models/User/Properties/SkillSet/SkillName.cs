using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.SkillSet
{
    public sealed class SkillName : IEquatable<SkillName>
    {
        public string Value { get; }

        [JsonConstructor]
        private SkillName(string value)
        {
            Value = value;
        }

        public static SkillName Create(string value)
        {
            ValidateName(value);
            return new SkillName(value.Trim());
        }

        // Validations
        private static void ValidateName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidSkillException("Skill name cannot be null or empty.", "InvalidSkillName");
        }

        // Equality
        public bool Equals(SkillName? other) =>
            other is not null &&
            StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as SkillName);
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value;
    }
}
