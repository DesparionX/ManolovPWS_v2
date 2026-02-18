using ManolovPWS_v2.Modules.Identity.User.Features.Shared.ReadModels;
using ManolovPWS_v2.Modules.Identity.User.Features.Shared.SharedProperties;

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

        private static List<Contact> MapContacts(Domain.Models.User.User user)
            => user.Contacts.ContactList.Select(c => new Contact(
                Network: c.Network,
                ProfileName: c.ProfileName,
                FullUrl: c.FullUrl.ToString()
                )).ToList();

        private static List<Skill> MapSkills(Domain.Models.User.User user)
            => user.Skills.Skills.Select(s => new Skill(
                Name: s.Name.Value,
                Level: s.Level.Value,
                Type: s.Type.ToString(),
                Category: s.Category.Name
                )).ToList();

        private static List<Language> MapLanguages(Domain.Models.User.User user)
            => user.Skills.Languages.Select(l => new Language(
                LanguageName: l.Language.Value,
                ReadingLevel: l.Level?.Reading.ToString(),
                WritingLevel: l.Level?.Writing.ToString(),
                SpeakingLevel: l.Level?.Speaking.ToString(),
                IsNative: l.IsNative
                )).ToList();

        private static List<Job> MapExperience(Domain.Models.User.User user)
            => user.Experience.Jobs.Select(j => new Job(
                Title: j.Title.Value,
                Company: j.Company.Value,
                Description: j.Description.Value,
                StartDate: j.Period.StartDate,
                EndDate: j.Period.EndDate
                )).ToList();

        private static List<Education> MapEducation(Domain.Models.User.User user)
            => user.EducationHistory.EducationEntries.Select(e => new Education(
                School: e.School.Name,
                Degree: e.Degree.Value,
                FieldOfStudy: e.FieldOfStudy.Value,
                StartDate: e.StudyPeriod.StartDate,
                EndDate: e.StudyPeriod.EndDate
                )).ToList();

        private static List<Certificate> MapCertificates(Domain.Models.User.User user)
            => user.Certificates.CertificatesList.Select(c => new Certificate(
                Title: c.Title.Value,
                Issuer: c.Issuer.Value,
                DateObtained: c.Date.Value,
                CredentialId: c.Credentials.CredentialId,
                CredentialUrl: c.Credentials.CredentialUrl.ToString()
                )).ToList();
    }
}
