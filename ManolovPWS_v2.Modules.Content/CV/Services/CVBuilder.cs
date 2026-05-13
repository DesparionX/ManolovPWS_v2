using ManolovPWS_v2.Domain.Models.Project;
using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Modules.Content.CV.Shared.ReadModels;
using ManolovPWS_v2.Modules.Identity.User.Maps;
using ManolovPWS_v2.Modules.Identity.User.Shared.SharedProperties;
using ManolovPWS_v2.Modules.Projects.Project.Maps;

namespace ManolovPWS_v2.Modules.Content.CV.Services
{
    public sealed class CVBuilder : ICVBuilder
    {
        public PublicCVReadModel Build(User user, IReadOnlyCollection<Project> projects)
        {
            var profilePicture = user.ProfilePicture?.Url.ToString();
            var fullName = user.Name.FullName;
            var gender = user.Gender.Value.ToString();
            var address = user.Address is not null ? new PublicAddress(
                Country: user.Address.Country,
                City: user.Address.City
            ) : null;
            var profession = user.Profession.ToString();
            var summary = user.Summary!.ToString();
            var workExperience = user.MapExperience();
            var cvProjects = projects.Select(p => p.ToCVReadModel()).ToList();
            var education = user.MapEducation();
            var certificates = user.MapCertificates();
            var skills = user.MapSkills();
            var languages = user.MapLanguages();
            var contacts = user.MapContacts();

            return new PublicCVReadModel(
                ProfilePictureUrl: profilePicture,
                FullName: fullName,
                Gender: gender,
                Address: address,
                Profession: profession,
                Summary: summary,
                WorkExperience: workExperience,
                Projects: cvProjects,
                Education: education,
                Certificates: certificates,
                Skills: skills,
                Languages: languages,
                Contacts: contacts
            );
        }
    }
}
