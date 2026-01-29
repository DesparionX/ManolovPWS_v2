using ManolovPWS_v2.Domain.Models.Post.Exceptions;

namespace ManolovPWS_v2.Domain.Models.Post.Properties
{
    public sealed class PostPublishedDate : IEquatable<PostPublishedDate>
    {
        public DateOnly Value { get; }

        private PostPublishedDate(DateOnly value)
        {
            Value = value;
        }

        public static PostPublishedDate Create(DateOnly value)
        {
            ValidatePublishedDate(value);

            return new(value);
        }

        // Validations
        private static void ValidatePublishedDate(DateOnly value)
        {
            if (value > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidPostPublishedDateException("Published date cannot be in the future.");
        }

        // Equality
        public bool Equals(PostPublishedDate? other) =>
            other is not null
            && DateOnly.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as PostPublishedDate);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
