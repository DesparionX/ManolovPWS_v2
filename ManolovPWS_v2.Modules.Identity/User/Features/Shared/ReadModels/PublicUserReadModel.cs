using ManolovPWS_v2.Modules.Identity.User.Features.Shared.SharedProperties;

namespace ManolovPWS_v2.Modules.Identity.User.Features.Shared.ReadModels
{
    public sealed record PublicUserReadModel(
        Guid Id,
        string UserName,
        string Email,
        string FirstName,
        string? MiddleName,
        string LastName,
        string? ProfilePictureUrl,
        DateOnly BirthDate,
        string Gender,
        IReadOnlyList<Contact> Contacts,
        IReadOnlyList<Skill> Skills,
        IReadOnlyList<Language> Languages,
        IReadOnlyList<Job> Experience,
        IReadOnlyList<Education> EducationHistory,
        IReadOnlyList<Certificate> Certificates
    );
}
