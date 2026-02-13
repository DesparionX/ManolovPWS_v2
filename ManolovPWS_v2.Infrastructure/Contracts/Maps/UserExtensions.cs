using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Domain.Models.User.Properties.Certificates;
using ManolovPWS_v2.Domain.Models.User.Properties.Contacts;
using ManolovPWS_v2.Domain.Models.User.Properties.Education;
using ManolovPWS_v2.Domain.Models.User.Properties.Experience;
using ManolovPWS_v2.Domain.Models.User.Properties.SkillSet;
using ManolovPWS_v2.Infrastructure.Exceptions;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;

namespace ManolovPWS_v2.Infrastructure.Contracts.Maps
{
    public static class UserExtensions
    {
        public static User ToDomain(this DbUser dbUser)
        {
            if (dbUser is null)
                throw new InfrastructureException("DbUser cannot be null", "InvalidDbUser");

            return User.Create(
                UserId.From(dbUser.Id.ToString()),
                UserName.Create(dbUser.UserName!),
                Name.Create(dbUser.FirstName, dbUser.MiddleName, dbUser.LastName),
                Email.Create(dbUser.Email!),
                BirthDate.Create(dbUser.BirthDate),
                Gender.FromString(dbUser.Gender),
                Contacts.From(dbUser.Contacts?.ContactList),
                UserPhoneNumber.CreateOrNull(dbUser.PhoneNumber),
                ProfilePicture.CreateOrNull(dbUser.ProfilePictureUrl),
                SkillSet.From(dbUser.Skills?.Skills, dbUser.Skills?.Languages),
                Experience.From(dbUser.Experience?.Jobs),
                EducationHistory.From(dbUser.EducationHistory?.EducationEntries),
                Certificates.From(dbUser.Certificates?.CertificatesList));
        }

        public static DbUser ToDbEntity(this User user)
        {
            if (user is null)
                throw new InfrastructureException("User cannot be null", "InvalidUser");

            var dbUser = new DbUser
            {
                Id = user.Id.Value,
                UserName = user.UserName.Value,
                FirstName = user.Name.FirstName,
                MiddleName = user.Name.MiddleName,
                LastName = user.Name.LastName,
                ProfilePictureUrl = user.ProfilePicture?.Url.ToString(),
                BirthDate = user.BirthDate.Value,
                Gender = user.Gender.Value.ToString(),
                Contacts = user.Contacts,
                Email = user.Email.Value,
                PhoneNumber = user.PhoneNumber?.Value,
                Skills = user.Skills,
                Experience = user.Experience,
                EducationHistory = user.EducationHistory,
                Certificates = user.Certificates
            };

            return dbUser;
        }

        public static IReadOnlyList<User> ToDomainList(this IReadOnlyList<DbUser> users)
            => users.Select(u => u.ToDomain()).ToList();

        public static IReadOnlyList<DbUser> ToDbEntityList(this IReadOnlyList<User> users)
            => users.Select(u => u.ToDbEntity()).ToList();
    }
}
