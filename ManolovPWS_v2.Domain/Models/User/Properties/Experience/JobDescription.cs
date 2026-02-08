using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Experience
{
    public sealed class JobDescription : IEquatable<JobDescription>
    {
        public string Value { get; }

        [JsonConstructor]
        private JobDescription(string description)
        {
            Value = description;
        }

        public static JobDescription Create(string description)
        {
            ValidateDescription(description);

            return new JobDescription(description);
        }

        // Validations
        private static void ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new InvalidExperienceException("Job description is null or white space", "InvalidJobDescription");
            if (description.Length > 5000)
                throw new InvalidExperienceException("Job exceeds the maximum characters limit.", "JobDescriptionTooLong");
        }

        // Equality
        public bool Equals(JobDescription? other) =>
            other is not null
            && StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as JobDescription);

        public override int GetHashCode() => Value.GetHashCode(StringComparison.InvariantCultureIgnoreCase);

        public override string ToString() => Value;
    }
}
