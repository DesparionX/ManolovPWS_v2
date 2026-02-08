using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Experience
{
    public sealed class JobTitle : IEquatable<JobTitle>
    {
        public string Value { get; }

        [JsonConstructor]
        private JobTitle(string value)
        {
            Value = value;
        }
        public static JobTitle Create(string value)
        {
            ValidateJobTitle(value);

            return new JobTitle(value);
        }

        // Validations
        private static void ValidateJobTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidExperienceException("Job title cannot be empty or whitespace.", "InvalidJobTitle");
            if (value.Length > 100)
                throw new InvalidExperienceException("Job title cannot exceed 100 characters.", "JobTitleTooLong");
        }

        // Equality
        public bool Equals(JobTitle? other) =>
            other is not null &&
            StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);
        public override bool Equals(object? obj) => Equals(obj as JobTitle);
        public override int GetHashCode() => Value.GetHashCode(StringComparison.InvariantCultureIgnoreCase);
        public override string ToString() => Value;
    }
}
