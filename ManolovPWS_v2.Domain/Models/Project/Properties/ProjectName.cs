using ManolovPWS_v2.Domain.Models.Project.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.Project.Properties
{
    public sealed class ProjectName : IEquatable<ProjectName>
    {
        public string Value { get; }

        private ProjectName(string value)
        {
            Value = value; 
        }

        public static ProjectName Create(string value)
        {
            ValidateProjectName(value);

            return new(value);
        }

        // Validations
        private static void ValidateProjectName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidProjectNameException("ProjectName cannot be null or empty.");
        }

        // Equality
        public bool Equals(ProjectName? other) => 
            other is not null
            && StringComparer.InvariantCultureIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as ProjectName);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
