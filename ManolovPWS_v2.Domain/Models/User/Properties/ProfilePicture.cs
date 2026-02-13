using ManolovPWS_v2.Domain.Models.Post.Exceptions;
using ManolovPWS_v2.Domain.Models.User.Exceptions;

namespace ManolovPWS_v2.Domain.Models.User.Properties
{
    public sealed class ProfilePicture : IEquatable<ProfilePicture>
    {
        public Uri Url { get; }

        private ProfilePicture(Uri url)
        {
            Url = url;
        }

        public static ProfilePicture Create(string url)
            => new(ValidatedPictureUrl(url));

        public static ProfilePicture? CreateOrNull(string? url)
            => string.IsNullOrWhiteSpace(url) ? null : Create(url);

        // Validations
        private static Uri ValidatedPictureUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                throw new InvalidUriException("Entered URL is invalid.");

            return uri;
        }

        // Equality
        public bool Equals(ProfilePicture? other) =>
            other is not null 
            && Url == other.Url;

        public override bool Equals(object? obj) =>
            Equals(obj as ProfilePicture);

        public override int GetHashCode() =>
            Url.GetHashCode();

        public override string ToString() => Url.ToString();
    }
}
