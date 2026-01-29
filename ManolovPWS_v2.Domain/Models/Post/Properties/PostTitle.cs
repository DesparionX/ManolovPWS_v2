using ManolovPWS_v2.Domain.Models.Post.Exceptions;

namespace ManolovPWS_v2.Domain.Models.Post.Properties
{
    public sealed class PostTitle : IEquatable<PostTitle>
    {
        public string Value { get; }

        private PostTitle(string value)
        {
            Value = value;
        }

        public static PostTitle Create(string value)
        {
            ValidatePostTitle(value);

            return new(value);
        }

        // Validations
        private static void ValidatePostTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidPostTitleException("PostTitle cannot be null or empty.");
        }

        // Equality
        public bool Equals(PostTitle? other) =>
            other is not null
            && StringComparer.InvariantCultureIgnoreCase.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as PostTitle);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
