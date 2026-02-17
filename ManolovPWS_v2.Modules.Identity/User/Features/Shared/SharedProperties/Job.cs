namespace ManolovPWS_v2.Modules.Identity.User.Features.Shared.SharedProperties
{
    public sealed record Job(
        string Title,
        string Company,
        string Description,
        DateOnly StartDate,
        DateOnly? EndDate
        );
}
