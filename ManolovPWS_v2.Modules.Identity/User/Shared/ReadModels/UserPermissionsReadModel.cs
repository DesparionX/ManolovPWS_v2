namespace ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels
{
    public sealed record UserPermissionsReadModel(
        string UserId,
        IReadOnlyList<string> Permissions
        );
}
