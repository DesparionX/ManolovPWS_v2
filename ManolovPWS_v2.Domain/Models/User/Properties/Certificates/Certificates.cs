using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Certificates
{
    public sealed class Certificates : IEquatable<Certificates>
    {
        private readonly List<Certificate> _certificates;

        public IReadOnlyList<Certificate> CertificatesList => _certificates;

        private Certificates(IEnumerable<Certificate> certificates)
        {
            _certificates = [.. certificates];
        }

        public static Certificates Create(IEnumerable<Certificate> certificates)
            => new(certificates);

        // Manipulations
        public static Certificates Empty() => new([]);

        internal Certificates AddCertificate(Certificate certificate)
            => new(_certificates.Append(certificate));

        internal Certificates RemoveCertificate(Certificate certificate)
            => new(_certificates.Where(c => c.Equals(certificate)));

        internal Certificates UpdateCertificate(Certificate oldCertificate, Certificate newCertificate)
            => new(_certificates.Select(c => c.Equals(oldCertificate) ? newCertificate : c));

        // Equality
        public bool Equals(Certificates? other) =>
            other is not null
            && _certificates.OrderBy(c => c.Title)
            .SequenceEqual(other._certificates.OrderBy(c => c.Title))
            && _certificates.Except(other._certificates).Any();

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
