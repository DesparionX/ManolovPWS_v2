using ManolovPWS_v2.Domain.Models.Project.Exceptions;

namespace ManolovPWS_v2.Domain.Models.Project.Properties
{
    public sealed class ProjectUploadedDate : IEquatable<ProjectUploadedDate>
    {
        public DateOnly Value { get; }

        private ProjectUploadedDate(DateOnly value)
        {
            Value = value;
        }

        public static ProjectUploadedDate Create(DateOnly value)
        {
            ValidateUploadedDate(value);

            return new(value);
        }

        // Validations
        private static void ValidateUploadedDate(DateOnly value)
        {
            if (value > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidProjectDateException("Uploaded date cannot be in the future.");
        }

        // Equality
        public bool Equals(ProjectUploadedDate? other) =>
            other is not null
            && DateOnly.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as ProjectUploadedDate);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
