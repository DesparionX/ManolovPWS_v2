namespace ManolovPWS_v2.Modules.Identity.User.Shared.SharedProperties
{
    public sealed record JobDto(
        string Title,
        string Company,
        string Description,
        DateOnly StartDate,
        DateOnly? EndDate
        );
}
