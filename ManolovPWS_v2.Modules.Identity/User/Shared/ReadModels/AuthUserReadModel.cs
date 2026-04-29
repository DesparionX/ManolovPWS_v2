namespace ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels
{
    public sealed record AuthUserReadModel(
        Guid Id,
        string UserName,
        string Email,
        string FirstName,
        string? MiddleName,
        string LastName,
        string? ProfilePictureUrl
        );
}
