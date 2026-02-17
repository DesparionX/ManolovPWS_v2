namespace ManolovPWS_v2.Modules.Identity.User.Features.Shared.SharedProperties
{
    public sealed record Education(
        string School,
        string Degree,
        string FieldOfStudy,
        DateOnly StartDate,
        DateOnly? EndDate
        );
}
