using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Certificates
{
    public sealed class Certificates : IEquatable<Certificates>
    {
        private readonly List<Certificate> _certificates;

        public IReadOnlyList<Certificate> Items => _certificates;

        [JsonConstructor]
        private Certificates(IReadOnlyList<Certificate> items)
        {
            _certificates = [.. items];
        }

        public static Certificates Create(IEnumerable<Certificate> certificates)
            => new(certificates.ToList());

        public static Certificates? From(IEnumerable<Certificate>? certificates)
            => certificates is not null ? new(certificates.ToList()) : null;

        // Manipulations
        public static Certificates Empty() => new([]);

        internal Certificates AddCertificate(Certificate certificate)
            => new(_certificates.Append(certificate).ToList());

        internal Certificates RemoveCertificate(Certificate certificate)
            => new(_certificates.Where(c => !c.Equals(certificate)).ToList());

        internal Certificates UpdateCertificate(Certificate oldCertificate, Certificate newCertificate)
            => new(_certificates.Select(c => c.Equals(oldCertificate) ? newCertificate : c).ToList());

        // Equality
        public bool Equals(Certificates? other) =>
            other is not null
            && _certificates.OrderBy(c => c.Title)
            .SequenceEqual(other._certificates.OrderBy(c => c.Title));

        public override bool Equals(object? obj) => Equals(obj as Certificates);

        public override int GetHashCode()
        {
            var hash = new HashCode();

            foreach (var cert in _certificates)
            {
                hash.Add(cert.GetHashCode());
            }

            return hash.ToHashCode();
        }
    }
}
