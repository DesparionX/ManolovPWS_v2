using ManolovPWS_v2.Domain.Models.Post.Exceptions;

namespace ManolovPWS_v2.Domain.Models.Post.Properties
{
    public sealed class PostId : IEquatable<PostId>
    {
        public Guid Value { get; }

        private PostId(Guid value)
        {
            Value = value;
        }

        public static PostId New() => new(Guid.NewGuid());

        public static PostId From(string value)
        {
            var id = ValidatePostId(value);
            return new(id);
        }

        // Validations
        private static Guid ValidatePostId(string value)
        {
            if (!Guid.TryParse(value.ToString(), out Guid id))
                throw new InvalidPostIdException("This is not a valid GUID.");

            if (id == Guid.Empty)
                throw new InvalidPostIdException("GUID cannot be null or empty.");

            return id;
        }

        // Equality
        public bool Equals(PostId? other) =>
            other is not null
            && Guid.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as PostId);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
