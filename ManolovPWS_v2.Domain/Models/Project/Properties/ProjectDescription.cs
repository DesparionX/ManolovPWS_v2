using ManolovPWS_v2.Domain.Models.Project.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.Project.Properties
{
    public sealed class ProjectDescription : IEquatable<ProjectDescription>
    {
        public string Value { get; }

        private ProjectDescription(string value)
        {
            Value = value;
        }

        public static ProjectDescription Create(string value)
        {
            ValidateProjectDescription(value);

            return new ProjectDescription(value);
        }
        // Validations
        private static void ValidateProjectDescription(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidProjectDescriptionException("Project description cannot be null or empty.");

            if (value.Length > 10000)
                throw new InvalidProjectDescriptionException("Project description must be less than 10 000 characters.");
        }

        // Equality
        public bool Equals(ProjectDescription? other) =>
            other is not null
            && StringComparer.InvariantCultureIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as ProjectDescription);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
