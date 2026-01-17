using ManolovPWS_v2.Domain.Models.User.Exceptions;

namespace ManolovPWS_v2.Domain.Models.User.Properties
{
    public sealed class UserId<TKey> : IEquatable<UserId<TKey>> where TKey : IEquatable<TKey>
    {
        public TKey Value { get; }
        private UserId(TKey value)
        {
            Value = value;
        }

        public static UserId<TKey> From(TKey value)
        {
            ValidateUserId(value);

            return new UserId<TKey>(value);
        }

        // Validations
        private static void ValidateUserId(TKey value)
        {
            if (value.Equals(default))
                throw new InvalidUserIdException("UserId cannot be null or default.");

            if (IsValidGuid(value))
                throw new InvalidUserIdException("UserId cannot be an empty GUID.");
        }
        private static bool IsValidGuid(TKey value)
        {
            if (typeof(TKey) != typeof(Guid))
                return true; // Not applicable

            if ((Guid.TryParse(value.ToString(), out Guid id)) && id != Guid.Empty)
                return true;

            return false;
        }

        // Equality
        public override bool Equals(object? obj) => Equals(obj as UserId<TKey>);
        public bool Equals(UserId<TKey>? other) => 
            other is not null 
            && Value.Equals(other.Value);
        public override int GetHashCode() => Value.GetHashCode();
    }
}
