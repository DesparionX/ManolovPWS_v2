using ManolovPWS_v2.Domain.Models.Post.Exceptions;

namespace ManolovPWS_v2.Domain.Models.Post.Properties
{
    public sealed class PostUpdatedDate : IEquatable<PostUpdatedDate>
    {
        public DateOnly Value { get; }

        private PostUpdatedDate(DateOnly value)
        {
            Value = value;
        }

        public static PostUpdatedDate Create(DateOnly value)
        {
            ValidateUpdatedDate(value);

            return new(value);
        }

        // Validations
        private static void ValidateUpdatedDate(DateOnly value)
        {
            if (value > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidPostUpdatedDateException("Updated date cannot be in the future.");
        }

        // Equality
        public bool Equals(PostUpdatedDate? other) =>
            other is not null
            && DateOnly.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as PostUpdatedDate);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
