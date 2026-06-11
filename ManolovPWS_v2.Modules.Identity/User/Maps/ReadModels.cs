using ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels;
using ManolovPWS_v2.Modules.Identity.User.Shared.SharedProperties;

namespace ManolovPWS_v2.Modules.Identity.User.Maps
{
    public static class ReadModels
    {
        public static PublicUserReadModel ToPublicUserRm(this Domain.Models.User.User user)
            => new(
                Id: user.Id.Value,
                UserName: user.UserName.Value,
                Email: user.Email.Value,
                FirstName: user.Name.FirstName,
                MiddleName: user.Name.MiddleName,
                LastName: user.Name.LastName,
                Country: user.Address?.Country,
                City: user.Address?.City,
                Summary: user.Summary?.Value,
                Profession: user.Profession.Value,
                ProfilePictureUrl: user.ProfilePicture?.Url.ToString(),
                BirthDate: user.BirthDate.Value,
                Gender: user.Gender.ToString(),
                Contacts: MapContacts(user),
                Skills: MapSkills(user),
                Languages: MapLanguages(user),
                Experience: MapExperience(user),
                EducationHistory: MapEducation(user),
                Certificates: MapCertificates(user)
                );

        public static PrivateUserReadModel ToPrivateUserRm(this Domain.Models.User.User user)
            => new(
                Id: user.Id.Value,
                UserName: user.UserName.Value,
                Email: user.Email.Value,
                FirstName: user.Name.FirstName,
                MiddleName: user.Name.MiddleName,
                LastName: user.Name.LastName,
                Country: user.Address?.Country,
                Region: user.Address?.Region,
                Municipality: user.Address?.Municipality,
                City: user.Address?.City,
                Street: user.Address?.Street,
                PostalCode: user.Address?.PostalCode,
                PhoneNumber: user.PhoneNumber?.Value,
                Summary: user.Summary?.Value,
                Profession: user.Profession.Value,
                ProfilePictureUrl: user.ProfilePicture?.Url.ToString(),
                BirthDate: user.BirthDate.Value,
                Gender: user.Gender.ToString(),
                Contacts: user.MapContacts(),
                Skills: user.MapSkills(),
                Languages: user.MapLanguages(),
                Experience: user.MapExperience(),
                EducationHistory: user.MapEducation(),
                Certificates: user.MapCertificates()
                );

        public static CompactUserReadModel ToCompactUserRm(this Domain.Models.User.User user)
            => new(
                Id: user.Id.Value,
                UserName: user.UserName.Value,
                Email: user.Email.Value,
                FirstName: user.Name.FirstName,
                MiddleName: user.Name.MiddleName,
                LastName: user.Name.LastName,
                ProfilePictureUrl: user.ProfilePicture?.Url.ToString()
                );

        public static List<ContactDto> MapContacts(this Domain.Models.User.User user)
            => user.Contacts.Items.Select(c => new ContactDto(
                Network: c.Network,
                ProfileName: c.ProfileName,
                FullUrl: c.FullUrl.ToString()
                )).ToList();

        public static List<SkillDto> MapSkills(this Domain.Models.User.User user)
            => user.Skills.Skills.Select(s => new SkillDto(
                Name: s.Name.Value,
                Level: s.Level.Value,
                Type: s.Type.ToString(),
                Category: s.Category.Name
                )).ToList();

        public static List<LanguageDto> MapLanguages(this Domain.Models.User.User user)
            => user.Skills.Languages.Select(l => new LanguageDto(
                LanguageName: l.Language.Value,
                ReadingLevel: l.Level?.Reading.ToString(),
                WritingLevel: l.Level?.Writing.ToString(),
                SpeakingLevel: l.Level?.Speaking.ToString(),
                IsNative: l.IsNative
                )).ToList();

        public static List<JobDto> MapExperience(this Domain.Models.User.User user)
            => user.Experience.Jobs.Select(j => new JobDto(
                Title: j.Title.Value,
                Company: j.Company.Value,
                Description: j.Description.Value,
                StartDate: j.Period.StartDate,
                EndDate: j.Period.EndDate
                )).ToList();

        public static List<EducationDto> MapEducation(this Domain.Models.User.User user)
            => user.EducationHistory.EducationEntries.Select(e => new EducationDto(
                SchoolName: e.School.Name,
                SchoolType: e.School.Type,
                Degree: e.Degree.Value,
                FieldOfStudy: e.FieldOfStudy.Value,
                StartDate: e.StudyPeriod.StartDate,
                EndDate: e.StudyPeriod.EndDate
                )).ToList();

        public static List<CertificateDto> MapCertificates(this Domain.Models.User.User user)
            => user.Certificates.Items.Select(c => new CertificateDto(
                Title: c.Title.Value,
                Issuer: c.Issuer.Value,
                DateObtained: c.Date.Value,
                CredentialId: c.Credentials.CredentialId,
                CredentialUrl: c.Credentials.CredentialUrl.ToString()
                )).ToList();
    }
}
