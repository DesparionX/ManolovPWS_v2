using ManolovPWS_v2.Modules.Identity.User.Shared.SharedProperties;
using ManolovPWS_v2.Modules.Projects.Project.Shared.ReadModels;

namespace ManolovPWS_v2.Modules.Content.CV.Shared.ReadModels
{
    public sealed record PublicCVReadModel(
        string? ProfilePictureUrl,
        string FullName,
        string Gender,
        PublicAddress? Address,
        string Profession,
        string Summary,
        IReadOnlyCollection<JobDto> WorkExperience,
        IReadOnlyCollection<CVProjectReadModel> Projects,
        IReadOnlyCollection<EducationDto> Education,
        IReadOnlyCollection<CertificateDto> Certificates,
        IReadOnlyCollection<SkillDto> Skills,
        IReadOnlyCollection<LanguageDto> Languages,
        IReadOnlyCollection<ContactDto> Contacts
        );
}
