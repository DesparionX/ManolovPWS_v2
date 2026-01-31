using ManolovPWS_v2.Domain.Models.User.Exceptions;

namespace ManolovPWS_v2.Domain.Models.User.Properties
{
    public sealed class UserId : IEquatable<UserId>
    {
        public Guid Value { get; }
        private UserId(Guid value)
        {
            Value = value;
        }

        public static UserId New() => new(Guid.NewGuid());

        public static UserId From(string value)
        {
            var id = ValidateUserId(value);

            return new(id);
        }

        // Validations
        private static Guid ValidateUserId(string value)
        {
            if (!Guid.TryParse(value.ToString(), out Guid id))
                throw new InvalidUserIdException("This is not a valid GUID.");

            if (id == Guid.Empty)
                throw new InvalidUserIdException("GUID cannot be null or empty.");

            return id;
        }

        // Equality
        public bool Equals(UserId? other) =>
            other is not null
            && Guid.Equals(Value, other.Value);

        public override bool Equals(object? obj) => Equals(obj as UserId);
        
        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
