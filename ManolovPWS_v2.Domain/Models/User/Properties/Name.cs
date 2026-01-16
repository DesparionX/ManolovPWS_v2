using ManolovPWS_v2.Domain.Models.User.Exceptions;

namespace ManolovPWS_v2.Domain.Models.User.Properties
{
    public sealed class Name : IEquatable<Name>
    {
        public string FirstName { get; }
        public string? MiddleName { get; }
        public string LastName { get; }

        public string FullName =>
            string.Join(" ",
                FirstName,
                string.IsNullOrWhiteSpace(MiddleName) ? string.Empty : MiddleName,
                LastName).Trim();

        private Name(string firstName, string? middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public static Name Create(string firstName, string? middleName, string lastName)
        {
            firstName = NormalizeName(firstName);
            middleName = NormalizeName(middleName);
            lastName = NormalizeName(lastName);

            ValidateName(firstName, middleName, lastName);

            return new Name(firstName, middleName!, lastName);
        }

        // Utility
        private static string NormalizeName(string? value) => value?.Trim();

        // Validations
        private static void ValidateName(string firstName,string middleName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new InvalidNameException("First name cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new InvalidNameException("Last name cannot be null or empty.");

            if (firstName.Length > 20 || lastName.Length > 20)
                throw new InvalidNameException("First or last name cannot exceed 20 characters.");

            if (!IsAlphabetic(firstName) || !IsAlphabetic(middleName) || !IsAlphabetic(lastName))
                throw new InvalidNameException("First, middle and last names must contain only alphabetic characters.");
        }

        private static bool IsAlphabetic(string value) =>
            value.All(char.IsLetter);

        // Equality
        public override bool Equals(object? obj) => Equals(obj as Name);
        public bool Equals(Name? other) => 
            other is not null &&
            StringComparer.OrdinalIgnoreCase.Equals(FirstName, other.FirstName) &&
            StringComparer.OrdinalIgnoreCase.Equals(MiddleName, other.MiddleName) &&
            StringComparer.OrdinalIgnoreCase.Equals(LastName, other.LastName);

        public override int GetHashCode() 
            => HashCode.Combine(
                FirstName.ToLowerInvariant(),
                MiddleName?.ToLowerInvariant() ?? "",
                LastName.ToLowerInvariant());
    }
}
