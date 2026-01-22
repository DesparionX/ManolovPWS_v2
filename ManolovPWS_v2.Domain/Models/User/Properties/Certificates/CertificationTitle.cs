using ManolovPWS_v2.Domain.Models.User.Exceptions;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Certificates
{
    public sealed class CertificationTitle : IEquatable<CertificationTitle>
    {
        public string Value { get; }

        private CertificationTitle(string value)
        {
            Value = value;
        }

        public static CertificationTitle Create(string value)
        {
            ValidateTitle(value);

            return new CertificationTitle(value);
        }

        // Validations
        private static void ValidateTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidCertificateException("Certification title cannot be null or empty.", "InvalidCertificationTitle");
        }

        // Equality
        public override bool Equals(object? obj) => Equals(obj as CertificationTitle);

        public bool Equals(CertificationTitle? other) => 
            other is not null 
            && StringComparer.InvariantCultureIgnoreCase.Equals(Value == other.Value);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}