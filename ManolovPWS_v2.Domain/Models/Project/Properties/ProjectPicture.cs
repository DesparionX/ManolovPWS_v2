using ManolovPWS_v2.Domain.Models.Project.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

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
        {
            var uri = ValidatePictureUrl(url);
            return new(uri);
        }

        // Validations
        private static Uri ValidatePictureUrl(string url)
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
