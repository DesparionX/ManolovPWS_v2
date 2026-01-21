using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User.Properties.Experience
{
    public sealed class CompanyName : IEquatable<CompanyName>
    {
        public string Value { get; }
        
        private CompanyName(string value)
        {
            Value = value;
        }

        public static CompanyName Create(string value)
        {
            ValidateCompanyName(value);

            return new CompanyName(value.Trim());
        }

        // Validations
        private static void ValidateCompanyName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidExperienceException("Company name cannot be null or empty.", "InvalidCompanyName");
            if (value.Length > 100)
                throw new InvalidExperienceException("Company name cannot exceed 100 characters.", "CompanyNameTooLong");
        }

        // Equality
        public override bool Equals(object? obj) => Equals(obj as CompanyName);
        
        public bool Equals(CompanyName? other) =>
            other is not null &&
            StringComparer.OrdinalIgnoreCase.Equals(Value, other.Value);

        public override int GetHashCode() => Value.GetHashCode(StringComparison.OrdinalIgnoreCase);
        
        public override string ToString() => Value;
    }
}
