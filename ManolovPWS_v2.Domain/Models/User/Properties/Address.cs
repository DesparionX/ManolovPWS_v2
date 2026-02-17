using ManolovPWS_v2.Domain.Models.User.Exceptions;
using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.User.Properties
{
    public sealed class Address : IEquatable<Address>
    {
        public string Country { get; }
        public string Region { get; }
        public string Municipality { get; }
        public string City { get; }
        public string Street { get; }
        public string PostalCode { get; }

        [JsonConstructor]
        private Address(string country, string region, string municipality, string city, string street, string postalCode)
        {
            Country = country;
            Region = region;
            Municipality = municipality;
            City = city;
            Street = street;
            PostalCode = postalCode;
        }

        public static Address Create(
            string country,
            string region,
            string municipality,
            string city,
            string street,
            string postalCode)
        {
            ValidateData(country, region, municipality, city, street, postalCode);

            return new(country, region, municipality, city, street, postalCode);
        }
        public static Address? CreateOrNull(Address? address)
        
            => address is null
                ? null
                : Create(
                    address.Country,
                    address.Region,
                    address.Municipality,
                    address.City,
                    address.Street,
                    address.PostalCode);
        
        // Validations
        private static void ValidateData(string country, string region, string municipality, string city, string street, string postalCode)
        {
            ValidateCountry(country);
            ValidateRegion(region);
            ValidateMunicipality(municipality);
            ValidateCity(city);
            ValidateStreet(street);
            ValidatePostalCode(postalCode);
        }
        private static void ValidateCountry(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new InvalidAddressException("Country cannot be null or empty.");

            if (country.Length > 20)
                throw new InvalidAddressException("Country cannot be longer than 20 characters.");
        }
        private static void ValidateRegion(string region)
        {
            if (string.IsNullOrWhiteSpace(region))
                throw new InvalidAddressException("Region cannot be null or empty.");
            if (region.Length > 40)
                throw new InvalidAddressException("Region cannot be longer than 40 characters.");
        }
        private static void ValidateMunicipality(string municipality)
        {
            if (string.IsNullOrWhiteSpace(municipality))
                throw new InvalidAddressException("Municipality cannot be null or empty.");

            if (municipality.Length > 40)
                throw new InvalidAddressException("Municipality cannot be longer than 40 characters.");
        }
        private static void ValidateCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new InvalidAddressException("City cannot be null or empty.");

            if (city.Length > 40)
                throw new InvalidAddressException("City cannot be longer than 40 characters.");
        }
        private static void ValidateStreet(string street)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new InvalidAddressException("Street cannot be null or empty.");

            if (street.Length > 100)
                throw new InvalidAddressException("Street cannot be longer than 100 characters.");
        }
        private static void ValidatePostalCode(string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
                throw new InvalidAddressException("Postal code cannot be null or empty.");

            if (postalCode.Length > 15)
                throw new InvalidAddressException("Postal code cannot be longer than 15 characters.");
        }

        // Equality

        public bool Equals(Address? other) =>
            other is not null
            && StringComparer.OrdinalIgnoreCase.Equals(Country, other.Country)
            && StringComparer.OrdinalIgnoreCase.Equals(Region, other.Region)
            && StringComparer.OrdinalIgnoreCase.Equals(Municipality, other.Municipality)
            && StringComparer.OrdinalIgnoreCase.Equals(City, other.City)
            && StringComparer.OrdinalIgnoreCase.Equals(Street, other.Street)
            && StringComparer.OrdinalIgnoreCase.Equals(PostalCode, other.PostalCode);

        public override bool Equals(object? obj) => Equals(obj as Address);

        public override int GetHashCode() => 
            HashCode.Combine(
                StringComparer.OrdinalIgnoreCase.GetHashCode(Country),
                StringComparer.OrdinalIgnoreCase.GetHashCode(Region),
                StringComparer.OrdinalIgnoreCase.GetHashCode(Municipality),
                StringComparer.OrdinalIgnoreCase.GetHashCode(City),
                StringComparer.OrdinalIgnoreCase.GetHashCode(Street),
                StringComparer.OrdinalIgnoreCase.GetHashCode(PostalCode)
            );
    }
}
