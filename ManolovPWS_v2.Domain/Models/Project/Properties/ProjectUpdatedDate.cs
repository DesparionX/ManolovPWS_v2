using ManolovPWS_v2.Domain.Models.Project.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.Project.Properties
{
    public sealed class ProjectUpdatedDate : IEquatable<ProjectUpdatedDate>
    {
        public DateOnly Value { get; }

        private ProjectUpdatedDate(DateOnly value)
        {
            Value = value;
        }

        public static ProjectUpdatedDate Create(DateOnly value)
        {
            ValidateUpdatedDate(value);

            return new(value);
        }

        public static ProjectUpdatedDate? CreateOrNull(DateOnly? value)
            => value is null || !value.HasValue
                ? null
                : Create(value.Value);

        // Validations
        private static void ValidateUpdatedDate(DateOnly value)
        {
            if (value > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidProjectDateException("Updated date cannot be in the future.");
        }

        // Equality
        public bool Equals(ProjectUpdatedDate? other) =>
            other is not null
            && DateOnly.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as ProjectUpdatedDate);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
