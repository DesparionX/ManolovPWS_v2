using ManolovPWS_v2.Domain.Models.User.Properties.Contacts;
using ManolovPWS_v2.Domain.Models.User.Properties.Experience;
using ManolovPWS_v2.Domain.Models.User.Properties.SkillSet;

namespace ManolovPWS_v2.Modules.Identity.User.Maps
{
    public static class DataTransferObjects
    {
        public static IEnumerable<Contact> ToDomainContacts(this IEnumerable<Shared.SharedProperties.Contact> contacts)
            => contacts.Select(c => Contact.Create(c.Network, c.ProfileName, c.FullUrl));

        public static IEnumerable<Skill> ToDomainSkills(this IEnumerable<Shared.SharedProperties.Skill> skills)
            => skills.Select(s => Skill.Create(
                SkillName.Create(s.Name),
                SkillTypeExtensions.FromString(s.Type),
                SkillCategory.Create(s.Category),
                SkillLevel.Create(s.Level)
            ));

        public static IEnumerable<LanguageSkill> ToDomainLanguages(this IEnumerable<Shared.SharedProperties.Language> languages)
            => languages.Select(l => l.IsNative
                ? LanguageSkill.CreateNative(LanguageName.Create(l.LanguageName))
                : LanguageSkill.CreateNonNative(
                    LanguageName.Create(l.LanguageName),
                    LanguageLevel.Create(
                            reading: CerfLevelExtensions.FromString(l.ReadingLevel!),
                            writing: CerfLevelExtensions.FromString(l.WritingLevel!),
                            speaking: CerfLevelExtensions.FromString(l.SpeakingLevel!)
                            )
                )
            );

        public static IEnumerable<Job> ToDomainExperience(this IEnumerable<Shared.SharedProperties.Job> experiences)
            => experiences.Select(e => Job.Create(
                JobTitle.Create(e.Title),
                CompanyName.Create(e.Company),
                JobDescription.Create(e.Description),
                JobPeriod.Create(
                    startDate: e.StartDate,
                    endDate: e.EndDate
                )
            ));
    }
}
