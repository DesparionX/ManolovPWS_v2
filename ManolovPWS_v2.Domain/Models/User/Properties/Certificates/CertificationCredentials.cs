using ManolovPWS_v2.Domain.Models.User.Exceptions;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Certificates
{
    public sealed class CertificationCredentials : IEquatable<CertificationCredentials>
    {
        public string CredentialId { get; }
        public Uri CredentialUrl { get; }

        private CertificationCredentials(string credentialId, Uri credentialUrl)
        {
            CredentialId = credentialId;
            CredentialUrl = credentialUrl;
        }

        public static CertificationCredentials Create(string credentialId, string credentialUrl)
        {
            var validUri = ValidateCredentials(credentialId, credentialUrl);

            return new CertificationCredentials(credentialId, validUri);
        }

        // Validations
        private static Uri ValidateCredentials(string credentialId, string credentialUrl)
        {
            if (string.IsNullOrWhiteSpace(credentialId))
                throw new InvalidCertificateException("Credential ID cannot be null or empty.", "InvalidCertificateCredentialId");

            if (!Uri.TryCreate(credentialUrl, UriKind.Absolute, out var uri))
                throw new InvalidUriException("Entered URL is invalid.");

            return uri;
        }

        // Equality
        public bool Equals(CertificationCredentials? other) =>
            other is not null 
            && StringComparer.OrdinalIgnoreCase.Equals(CredentialId, other.CredentialId)
            && StringComparer.OrdinalIgnoreCase.Equals(CredentialUrl, other.CredentialUrl);

        public override bool Equals(object? obj) => 
            Equals(obj as CertificationCredentials);

        public override int GetHashCode() => CredentialId.GetHashCode();
    }
}