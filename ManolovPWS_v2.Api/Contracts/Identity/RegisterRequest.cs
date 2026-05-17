namespace ManolovPWS_v2.Api.Contracts.Identity
{
    public sealed record RegisterRequest(
        string UserName,
        string Email,
        string Password,
        string FirstName,
        string? MiddleName,
        string Profession,
        string LastName,
        string Gender,
        DateOnly BirthDate
        );
}
