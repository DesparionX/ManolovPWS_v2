namespace ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels
{
    public sealed record UserRolesReadModel(
        string UserId,
        IReadOnlyList<string> Roles
        );
}
