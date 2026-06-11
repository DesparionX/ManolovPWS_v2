using ManolovPWS_v2.Modules.Identity.User.Shared.SharedProperties;

namespace ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels
{
    public sealed record PublicUserReadModel(
        Guid Id,
        string UserName,
        string Email,
        string FirstName,
        string? MiddleName,
        string LastName,
        string? Country,
        string? City,
        string? Summary,
        string? Profession,
        string? ProfilePictureUrl,
        DateOnly BirthDate,
        string Gender,
        IReadOnlyList<ContactDto> Contacts,
        IReadOnlyList<SkillDto> Skills,
        IReadOnlyList<LanguageDto> Languages,
        IReadOnlyList<JobDto> Experience,
        IReadOnlyList<EducationDto> EducationHistory,
        IReadOnlyList<CertificateDto> Certificates
    );
}
