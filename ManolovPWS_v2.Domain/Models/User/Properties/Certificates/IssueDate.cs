using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Certificates
{
    public sealed class IssueDate : IEquatable<IssueDate>
    {
        public DateOnly Value { get; }

        [JsonConstructor]
        private IssueDate(DateOnly date)
        {
            Value = date;
        }

        public static IssueDate Create(DateOnly date)
        {
            ValidateDate(date);

            return new IssueDate(date);
        }

        // Validations
        private static void ValidateDate(DateOnly date)
        {
            if (date > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidCertificateException("Issue date cannot be in the future.", "InvalidCertificationDate");
        }

        // Equality
        public bool Equals(IssueDate? other) =>
            other is not null
            && Value == other.Value;

        public override bool Equals(object? obj) => Equals(obj as IssueDate);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}