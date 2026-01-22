using ManolovPWS_v2.Domain.Models.User.Exceptions;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Education
{
    public sealed class School : IEquatable<School>
    {
        public string Name { get; }
        public string Type { get; }

        private School(string name, string type)
        {
            Name = name;
            Type = type;
        }
        public static School Create(string name, string type)
        {
            ValidateSchool(name, type);

            return new School(name, type);
        }

        // Validations
        private static void ValidateSchool(string name, string type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidEducationException("School name cannot be empty.", "InvalidSchoolName");
            if (string.IsNullOrWhiteSpace(type))
                throw new InvalidEducationException("School type cannot be empty.", "InvalidSchoolType");
        }

        // Equality
        public bool Equals(School? other) =>
            other is not null
            && StringComparer.OrdinalIgnoreCase.Equals(Name, other.Name)
            && StringComparer.OrdinalIgnoreCase.Equals(Type, other.Type);

        public override bool Equals(object? obj) => Equals(obj as School);

        public override int GetHashCode() => HashCode.Combine(Name.GetHashCode(), Type.GetHashCode());

        public override string ToString() => Name;
    }
}