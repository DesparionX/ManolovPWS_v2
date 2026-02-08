using ManolovPWS_v2.Domain.Models.Post.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Post.Properties.PostContent
{
    public sealed class PostContext : IEquatable<PostContext>
    {
        public string Value { get; }

        [JsonConstructor]
        private PostContext(string value)
        {
            Value = value;
        }

        public static PostContext Create(string value)
        {
            ValidatePostContext(value);

            return new(value);
        }
        // Validations
        private static void ValidatePostContext(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidPostContentException("Post context cannot be null or empty.", "InvalidContext");

            if (value.Length > 10000)
                throw new InvalidPostContentException("Post context must be less than 10 000 characters.", "ContextTooLong");
        }

        // Equality
        public bool Equals(PostContext? other) =>
            other is not null
            && StringComparer.InvariantCultureIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as PostContext);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
