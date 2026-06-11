namespace ManolovPWS_v2.Api.Contracts.Profile
{
    public sealed record UpdateNameRequest(
        string FirstName,
        string LastName,
        string? MiddleName = default
    );
}
