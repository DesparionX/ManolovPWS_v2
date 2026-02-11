using ManolovPWS_v2.Domain.Models.Project.Exceptions;

namespace ManolovPWS_v2.Domain.Models.Project.Properties
{
    public sealed class ProjectState : IEquatable<ProjectState>
    {
        public ProjectStateType Value { get; }
        public bool IsInDevelopment => Value == ProjectStateType.InDevelopment;
        public bool IsFinished => Value == ProjectStateType.Finished;
        public bool IsFrozen => Value == ProjectStateType.Frozen;
        public bool IsAbandoned => Value == ProjectStateType.Abandoned;

        private ProjectState(ProjectStateType state)
        {
            Value = state;
        }

        public static ProjectState InDevelopment => new(ProjectStateType.InDevelopment);
        public static ProjectState Finished => new(ProjectStateType.Finished);
        public static ProjectState Frozen => new(ProjectStateType.Frozen);
        public static ProjectState Abandoned => new(ProjectStateType.Abandoned);

        public static ProjectState FromString(string value)
            => new(Parse(value));

        // Validations
        private static ProjectStateType Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new InvalidProjectStateException("State cannot be null or empty.");

            if (!Enum.TryParse<ProjectStateType>(value, ignoreCase: true, out var parsed)
                || !Enum.IsDefined(parsed))
                throw new InvalidProjectStateException("Invalid project state.");

            return parsed;
        }

        // Equality
        public bool Equals(ProjectState? other) => 
            other is not null 
            && Enum.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as ProjectState);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
