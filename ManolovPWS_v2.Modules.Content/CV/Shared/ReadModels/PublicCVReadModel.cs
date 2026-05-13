using ManolovPWS_v2.Modules.Content.CV.Services;
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
        IReadOnlyCollection<Job> WorkExperience,
        IReadOnlyCollection<CVProjectReadModel> Projects,
        IReadOnlyCollection<Education> Education,
        IReadOnlyCollection<Certificate> Certificates,
        IReadOnlyCollection<Skill> Skills,
        IReadOnlyCollection<Language> Languages,
        IReadOnlyCollection<Contact> Contacts
        );
}
