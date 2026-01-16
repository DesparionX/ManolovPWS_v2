using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User.Properties
{
    public sealed class BirthDate : IEquatable<BirthDate>
    {
        public DateOnly Value { get; }

        private BirthDate(DateOnly value)
        {
            Value = value;
        }

        public static BirthDate Create(DateOnly value)
        {
            ValidateBirthDate(value);

            return new BirthDate(value);
        }

        // Validations
        private static void ValidateBirthDate(DateOnly value)
        {
            if (value > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new InvalidBirthDateException("Birth date cannot be in the future.");

            if (value < DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-120)))
                throw new InvalidBirthDateException("No way you are 120yo :D !");
        }

        // Equality
        public bool Equals(BirthDate? other) =>
            other is not null && Value.Equals(other.Value);

        public override bool Equals(object? obj) => Equals(obj as BirthDate);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
