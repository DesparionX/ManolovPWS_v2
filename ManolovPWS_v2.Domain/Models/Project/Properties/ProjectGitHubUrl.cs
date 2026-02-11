using ManolovPWS_v2.Domain.Models.Project.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.Project.Properties
{
    public sealed class ProjectGitHubUrl : IEquatable<ProjectGitHubUrl>
    {
        public Uri Value { get; }

        private ProjectGitHubUrl(Uri value)
        {
            Value = value;
        }

        public static ProjectGitHubUrl Create(string url)
            => new(ValidatedGitHubUrl(url));

        public static ProjectGitHubUrl? CreateOrNull(string? url)
            => string.IsNullOrWhiteSpace(url) ? null : Create(url);

        // Validations
        private static Uri ValidatedGitHubUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                throw new InvalidProjectGitHubUrlException("Entered URL is invalid.");

            return uri;
        }

        // Equality
        public bool Equals(ProjectGitHubUrl? other) =>
            other is not null
            && Uri.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as ProjectGitHubUrl);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
