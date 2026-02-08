using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Certificates
{
    public sealed class Certificate : IEquatable<Certificate>
    { 
        public CertificationTitle Title { get; }
        public CertificationIssuer Issuer { get; }
        public IssueDate Date { get; }
        public CertificationCredentials Credentials { get; }

        [JsonConstructor]
        private Certificate(
            CertificationTitle title,
            CertificationIssuer issuer,
            IssueDate date,
            CertificationCredentials credentials)
        {
            Title = title;
            Issuer = issuer;
            Date = date;
            Credentials = credentials;
        }

        public static Certificate Create(
            CertificationTitle title,
            CertificationIssuer issuer,
            IssueDate date,
            CertificationCredentials credentials)
            => new(title, issuer, date, credentials);

        // Equality
        public bool Equals(Certificate? other) =>
            other is not null &&
            Title.Equals(other.Title) &&
            Issuer.Equals(other.Issuer) &&
            Date.Equals(other.Date) &&
            Credentials.Equals(other.Credentials);

        public override bool Equals(object? obj) => Equals(obj as Certificate);

        public override int GetHashCode() =>
            HashCode.Combine(
                Title.GetHashCode(),
                Issuer.GetHashCode(),
                Date.GetHashCode(),
                Credentials.GetHashCode()
                );

        public override string ToString() => Title.ToString();
    }
}
