using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Contacts
{
    public sealed class Contact : IEquatable<Contact>
    {
        public string Network { get; }
        public string ProfileName { get; }
        public Uri FullUrl { get; }

        [JsonConstructor]
        private Contact(string network, string profileName, Uri fullUrl)
        {
            Network = network;
            ProfileName = profileName;
            FullUrl = fullUrl;
        }

        public static Contact Create(string network, string profileName, string fullUrl)
        {
            ValidateNetwork(network);
            ValidateProfileName(profileName);
            var url = ValidateUrl(fullUrl);

            return new(network, profileName, url);
        }

        // Validations
        private static void ValidateNetwork(string network)
        {
            if (string.IsNullOrWhiteSpace(network))
                throw new InvalidContactException("Network cannot be null or empty.", "InvalidNetworkName");
        }

        private static void ValidateProfileName(string profileName)
        {
            if (string.IsNullOrWhiteSpace(profileName))
                throw new InvalidContactException("Profile name cannot be null or empty.", "InvalidProfileName");
        }

        private static Uri ValidateUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                throw new InvalidContactException("Entered URL is invalid.", "InvalidContactUrl");

            return uri;
        }

        // Equality
        public bool Equals(Contact? other) =>
            other is not null
            && StringComparer.OrdinalIgnoreCase.Equals(Network, other.Network)
            && StringComparer.OrdinalIgnoreCase.Equals(ProfileName, other.ProfileName)
            && Uri.Equals(FullUrl, other.FullUrl);

        public override bool Equals(object? obj) => Equals(obj as Contact);

        public override int GetHashCode()
            => HashCode.Combine(
                StringComparer.OrdinalIgnoreCase.GetHashCode(Network),
                StringComparer.OrdinalIgnoreCase.GetHashCode(ProfileName),
                FullUrl.GetHashCode());
    }
}
