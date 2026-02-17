namespace ManolovPWS_v2.Modules.Identity.User.Features.Shared.SharedProperties
{
    public sealed record Skill(
        string Name,
        int Level,
        string Type,
        string Category
        );
}
