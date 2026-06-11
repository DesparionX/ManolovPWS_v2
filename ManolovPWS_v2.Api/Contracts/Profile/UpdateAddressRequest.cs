namespace ManolovPWS_v2.Api.Contracts.Profile
{
    public sealed record UpdateAddressRequest(
        string Country,
        string Region,
        string Municipality,
        string City,
        string Street,
        string PostalCode
        );
}
