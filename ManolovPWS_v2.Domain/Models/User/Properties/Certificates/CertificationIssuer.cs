using ManolovPWS_v2.Domain.Models.User.Exceptions;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Certificates
{
    public sealed class CertificationIssuer : IEquatable<CertificationIssuer>
    {
        public string Value { get; }

        private CertificationIssuer(string value)
        {
            Value = value;
        }

        // Validations
        private static void ValidateIssuer(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidCertificateException("Certification issuer cannot be null or empty.", "InvalidIssuer");
        }

        // Equality
        public override bool Equals(object? obj) => Equals(obj as CertificationIssuer);

        public bool Equals(CertificationIssuer? other) =>
            other is not null
            && StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}