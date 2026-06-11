namespace ManolovPWS_v2.Modules.Identity.User.Shared.SharedProperties
{
    public sealed record EducationDto(
        string SchoolName,
        string SchoolType,
        string Degree,
        string FieldOfStudy,
        DateOnly StartDate,
        DateOnly? EndDate
        );
}
