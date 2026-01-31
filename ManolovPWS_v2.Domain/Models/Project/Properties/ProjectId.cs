using ManolovPWS_v2.Domain.Models.Project.Exceptions;
using ManolovPWS_v2.Domain.Models.User.Exceptions;

namespace ManolovPWS_v2.Domain.Models.Project.Properties
{
    public sealed class ProjectId : IEquatable<ProjectId>
    {
        public Guid Value { get; }

        private ProjectId(Guid value)
        {
            Value = value;
        }

        public static ProjectId New() => new(Guid.NewGuid());

        public static ProjectId From(string value)
        {
            var id = ValidateProjectId(value);
            return new(id);
        }

        // Validations
        private static Guid ValidateProjectId(string value)
        {
            if (!Guid.TryParse(value.ToString(), out Guid id))
                throw new InvalidProjectIdException("This is not a valid GUID.");

            if (id == Guid.Empty)
                throw new InvalidProjectIdException("GUID cannot be null or empty.");

            return id;
        }

        // Equality
        public bool Equals(ProjectId? other) => 
            other is not null
            && Guid.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as ProjectId);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
