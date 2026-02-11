using ManolovPWS_v2.Domain.Models.Project.Exceptions;

namespace ManolovPWS_v2.Domain.Models.Project.Properties
{
    public sealed class ProjectPicture : IEquatable<ProjectPicture>
    {
        public Uri Value { get; }

        private ProjectPicture(Uri value)
        {
            Value = value;
        }

        public static ProjectPicture Create(string url)
            => new(ValidatedPictureUrl(url));

        public static ProjectPicture? CreateOrNull(string? url)
            => string.IsNullOrWhiteSpace(url) ? null : Create(url);

        // Validations
        private static Uri ValidatedPictureUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                throw new InvalidProjectGalleryException("Entered URL is invalid.", "InvalidProjectPicture");

            return uri;
        }

        // Equality
        public bool Equals(ProjectPicture? other) =>
            other is not null
            && Uri.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as ProjectPicture);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
