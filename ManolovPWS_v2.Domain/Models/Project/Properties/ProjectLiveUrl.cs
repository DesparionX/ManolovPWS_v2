using ManolovPWS_v2.Domain.Models.Project.Exceptions;
using ManolovPWS_v2.Domain.Models.User.Exceptions;

namespace ManolovPWS_v2.Domain.Models.Project.Properties
{
    public sealed class ProjectLiveUrl : IEquatable<ProjectLiveUrl>
    {
        public Uri Value { get; }

        private ProjectLiveUrl(Uri value)
        {
            Value = value;
        }

        public static ProjectLiveUrl Create(string url)
            => new(ValidatedLiveUrl(url));

        public static ProjectLiveUrl? CreateOrNull(string? url)
            => string.IsNullOrWhiteSpace(url) ? null : Create(url);

        // Validations
        private static Uri ValidatedLiveUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                throw new InvalidProjectLiveUrlException("Entered URL is invalid.");

            return uri;
        }

        // Equality
        public bool Equals(ProjectLiveUrl? other) =>
            other is not null
            && Uri.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as ProjectLiveUrl);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}