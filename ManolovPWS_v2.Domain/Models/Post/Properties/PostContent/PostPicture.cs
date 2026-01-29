using ManolovPWS_v2.Domain.Models.Post.Exceptions;

namespace ManolovPWS_v2.Domain.Models.Post.Properties.PostContent
{
    public sealed class PostPicture : IEquatable<PostPicture>
    {
        public Uri Value { get; }

        private PostPicture(Uri value)
        {
            Value = value;
        }

        public static PostPicture Create(string url)
        {
            var uri = ValidatePictureUrl(url);
            return new(uri);
        }

        // Validations
        private static Uri ValidatePictureUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                throw new InvalidPostContentException("Entered URL is invalid.", "InvalidPostPicture");

            return uri;
        }

        // Equality
        public bool Equals(PostPicture? other) =>
            other is not null
            && Uri.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as PostPicture);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
