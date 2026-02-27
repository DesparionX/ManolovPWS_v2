using ManolovPWS_v2.Modules.Identity.User.Shared.SharedProperties;

namespace ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels
{
    public sealed record PrivateUserReadModel(
        Guid Id,
        string UserName,
        string Email,
        string FirstName,
        string? MiddleName,
        string LastName,
        string? Country,
        string? Region,
        string? Municipality,
        string? City,
        string? Street,
        string? PostalCode,
        string? PhoneNumber,
        string? Summary,
        string? Profession,
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
