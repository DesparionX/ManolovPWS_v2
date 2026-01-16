using ManolovPWS_v2.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User.Properties
{
    public sealed class UserName : IEquatable<UserName>
    {
        public string Value { get; }
        private UserName(string value)
        {
            Value = value;
        }

        public static UserName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidUserNameException("User name cannot be null or whitespace.");
            }
            if (value.Length < 3 || value.Length > 20)
            {
                throw new InvalidUserNameException("User name must be between 3 and 20 characters long.");
            }
            return new UserName(value);
        }

        public override bool Equals(object? obj) => Equals(obj as UserName);

        public bool Equals(UserName? other) =>
            other is not null 
            && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
