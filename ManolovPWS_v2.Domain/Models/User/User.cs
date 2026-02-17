using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Domain.Models.User.Properties.Certificates;
using ManolovPWS_v2.Domain.Models.User.Properties.Contacts;
using ManolovPWS_v2.Domain.Models.User.Properties.Education;
using ManolovPWS_v2.Domain.Models.User.Properties.Experience;
using ManolovPWS_v2.Domain.Models.User.Properties.SkillSet;

namespace ManolovPWS_v2.Domain.Models.User
{
    public sealed class User : IEntity<UserId>
    {
        public UserId Id { get; private set; }
        public UserName UserName { get; }
        public Name Name { get; }
        public Email Email { get; }
        public Profession Profession { get; }
        public Summary? Summary { get; }
        public UserPhoneNumber? PhoneNumber { get; }
        public Address? Address { get; }
        public ProfilePicture? ProfilePicture { get; }
        public BirthDate BirthDate { get; }
        public Gender Gender { get; }
        public Contacts Contacts { get; }
        public SkillSet Skills { get; }
        public Experience Experience { get; }
        public EducationHistory EducationHistory { get; }
        public Certificates Certificates { get; }

#pragma warning disable S107 // Constructor has too many parameters
        private User(
            UserId id,
            UserName userName,
            Name name,
            Email email,
            Profession profession,
            Summary? summary,
            UserPhoneNumber? phoneNumber,
            Address? address,
            BirthDate birthDate,
            Gender gender,
            Contacts contacts,
            ProfilePicture? profilePicture,
            SkillSet skills,
            Experience experience,
            EducationHistory educationHistory,
            Certificates certificates)
        {
            Id = id;
            UserName = userName;
            Name = name;
            Email = email;
            Profession = profession;
            Summary = summary;
            PhoneNumber = phoneNumber;
            Address = address;
            BirthDate = birthDate;
            ProfilePicture = profilePicture;
            Gender = gender;
            Contacts = contacts;
            Skills = skills;
            Experience = experience;
            EducationHistory = educationHistory;
            Certificates = certificates;
        }

        private User With(
            UserName? userName = default,
            Name? name = default,
            Email? email = default,
            Profession? profession = default,
            Summary? summary = default,
            UserPhoneNumber? phoneNumber = default,
            Address? address = default,
            BirthDate? birthDate = default,
            ProfilePicture? picture = default,
            Gender? gender = default,
            Contacts? contacts = default,
            SkillSet? skills = default,
            Experience? experience = default,
            EducationHistory? educationHistory = default,
            Certificates? certificates = default
            )
            => new(
                id: Id,
                userName: userName ?? UserName,
                name: name ?? Name,
                email: email ?? Email,
                profession: profession ?? Profession,
                summary: summary ?? Summary,
                phoneNumber: phoneNumber ?? PhoneNumber,
                address: address ?? Address,
                birthDate: birthDate ?? BirthDate,
                gender: gender ?? Gender,
                contacts: contacts ?? Contacts,
                profilePicture: picture ?? ProfilePicture,
                skills: skills ?? Skills,
                experience: experience ?? Experience,
                educationHistory: educationHistory ?? EducationHistory,
                certificates: certificates ?? Certificates
                );

        public static User Create(
            UserId id,
            UserName userName,
            Name name,
            Profession profession,
            Email email,
            BirthDate birthDate,
            Gender gender,
            Contacts? contacts,
            Summary? summary = default,
            UserPhoneNumber? phoneNumber = default,
            Address? address = default,
            ProfilePicture? profilePicture = default,
            SkillSet? skills = default,
            Experience? experience = default,
            EducationHistory? educationHistory = default,
            Certificates? certificates = default
            )
            => new(
                id: id,
                userName: userName,
                name: name,
                profession: profession,
                summary: summary,
                email: email,
                phoneNumber: phoneNumber,
                address: address,
                birthDate: birthDate,
                gender: gender,
                contacts: contacts ?? Contacts.Empty(),
                profilePicture: profilePicture,
                skills: skills ?? SkillSet.Empty(),
                experience: experience ?? Experience.Empty(),
                educationHistory: educationHistory ?? EducationHistory.Empty(),
                certificates: certificates ?? Certificates.Empty()
                );

        // User manipulations
        public User UpdateUserName(UserName newUserName)
        {
            if (UserName.Equals(newUserName)) return this;

            return With(userName: newUserName);
        }
        public User UpdateName(Name newName)
        {
            if (Name.Equals(newName)) return this;

            return With(name: newName);
        }
        public User UpdateEmail(Email newEmail)
        {
            if (Email.Equals(newEmail)) return this;

            return With(email: newEmail);
        }
        public User UpdateProfession(Profession newProfession)
        {
            if (Profession.Equals(newProfession)) return this;

            return With(profession: newProfession);
        }
        public User UpdateSummary(Summary newSummary)
        {
            if (Summary is not null && Summary.Equals(newSummary))
                return this;

            return With(summary: newSummary);
        }
        public User UpdatePhoneNumber(UserPhoneNumber newPhoneNumber)
        {
            if (PhoneNumber is not null && PhoneNumber.Equals(newPhoneNumber))
                return this;

            return With(phoneNumber: newPhoneNumber);
        }
        public User UpdateAddress(Address newAddress)
        {
            if (Address is not null && Address.Equals(newAddress))
                return this;

            return With(address: newAddress);
        }
        public User UpdateBirthDate(BirthDate newBirthDate)
        {
            if (BirthDate.Equals(newBirthDate)) return this;

            return With(birthDate: newBirthDate);
        }
        public User UpdateProfilePicture(ProfilePicture newProfilePicture)
        {
            if (ProfilePicture is not null && ProfilePicture.Equals(newProfilePicture))
                return this;

            return With(picture: newProfilePicture);
        }
        public User ChangeGender(Gender newGender)
        {
            if (Gender.Equals(newGender)) return this;

            return With(gender: newGender);
        }

        // SkillSet - Skills
        public User ClearSkills()
        {
            var clearedSkills = Skills.ClearSkills();

            if (Skills.Equals(clearedSkills)) return this;

            return With(skills: clearedSkills);
        }
        public User ReplaceSkills(IEnumerable<Skill> newSkills)
        {
            var updatedSkills = Skills.ReplaceSkills(newSkills);

            if (Skills.Equals(updatedSkills)) return this;

            return With(skills: updatedSkills);
        }
        public User AddSkill(Skill skill)
        {
            var updatedSkills = Skills.AddSkill(skill: skill);

            if (Skills.Equals(updatedSkills)) return this;

            return With(skills: updatedSkills);
        }
        public User UpdateSkill(Skill oldSkill, Skill newSkill)
        {
            var updatedSkills = Skills.UpdateSkill(oldSkill: oldSkill, newSkill: newSkill);

            if (Skills.Equals(updatedSkills)) return this;

            return With(skills: updatedSkills);
        }
        public User RemoveSkill(Skill skillToRemove)
        {
            var updatedSkills = Skills.RemoveSkill(skillToRemove);

            if (Skills.Equals(updatedSkills)) return this;

            return With(skills: updatedSkills);
        }

        // Contacts
        public User ClearContacts()
            => With(contacts: Contacts.Empty());
        public User AddContact(Contact contact)
        {
            var updatedContacts = Contacts.Add(contact: contact);

            if (Contacts.Equals(updatedContacts)) return this;

            return With(contacts: updatedContacts);
        }
        public User UpdateContact(Contact oldContact, Contact newContact)
        {
            var updatedContacts = Contacts.Update(oldContact: oldContact, newContact: newContact);

            if (Contacts.Equals(updatedContacts)) return this;

            return With(contacts: updatedContacts);
        }
        public User RemoveContact(Contact contactToRemove)
        {
            var updatedContacts = Contacts.Remove(contactToRemove);

            if (Contacts.Equals(updatedContacts)) return this;

            return With(contacts: updatedContacts);
        }
        public User ReplaceContacts(IEnumerable<Contact> newContacts)
        {
            var updatedContacts = Contacts.Create(newContacts);

            if (Contacts.Equals(updatedContacts)) return this;

            return With(contacts: updatedContacts);
        }

        // SkillSet - Languages
        public User ClearLanguages()
        {
            var clearedLanguages = Skills.ClearLanguages();

            if (Skills.Equals(clearedLanguages)) return this;

            return With(skills: clearedLanguages);
        }
        public User ReplaceLanguages(IEnumerable<LanguageSkill> newLanguages)
        {
            var updatedSkills = Skills.ReplaceLanguages(newLanguages);

            if (Skills.Equals(updatedSkills)) return this;

            return With(skills: updatedSkills);
        }
        public User AddLanguage(LanguageSkill language)
        {
            var updatedSkills = Skills.AddLanguage(language: language);

            if (Skills.Equals(updatedSkills)) return this;

            return With(skills: updatedSkills);
        }
        public User UpdateLanguage(LanguageSkill oldLanguage, LanguageSkill newLanguage)
        {
            var updatedSkills = Skills.UpdateLanguage(oldLanguage: oldLanguage, newLanguage: newLanguage);

            if (Skills.Equals(updatedSkills)) return this;

            return With(skills: updatedSkills);
        }
        public User RemoveLanguage(LanguageSkill languageToRemove)
        {
            var updatedSkills = Skills.RemoveLanguage(languageToRemove);

            if (Skills.Equals(updatedSkills)) return this;

            return With(skills: updatedSkills);
        }

        // Experience
        public User ClearExperience() 
            => With(experience: Experience.Empty());

        public User ReplaceExperience(Experience newExperience)
        {
            if (Experience.Equals(newExperience)) return this;

            return With(experience: newExperience);
        }
        public User AddJob(Job job)
        {
            var updatedExperience = Experience.AddJob(job: job);

            if (Experience.Equals(updatedExperience)) return this;

            return With(experience: updatedExperience);
        }
        public User UpdateJob(Job oldJob, Job newJob)
        {
            var updatedExperience = Experience.UpdateJob(oldJob: oldJob, newJob: newJob);

            if (Experience.Equals(updatedExperience)) return this;

            return With(experience: updatedExperience);
        }
        public User RemoveJob(Job jobToRemove)
        {
            var updatedExperience = Experience.RemoveJob(jobToRemove);

            if (Experience.Equals(updatedExperience)) return this;

            return With(experience: updatedExperience);
        }

        // Education
        public User ClearEducation()
            => With(educationHistory: EducationHistory.Empty());

        public User ReplaceEducation(EducationHistory newEducationHistory)
        {
            if (EducationHistory.Equals(newEducationHistory)) return this;

            return With(educationHistory: newEducationHistory);
        }
        public User AddEducation(Education education)
        {
            var updatedEducationHistory = EducationHistory.AddEducationEntry(education);

            if (EducationHistory.Equals(updatedEducationHistory)) return this;

            return With(educationHistory: updatedEducationHistory);
        }
        public User UpdateEducation(Education oldEducation, Education newEducation)
        {
            var updatedEducationHistory = EducationHistory.UpdateEducationEntry(oldEducation, newEducation);

            if (EducationHistory.Equals(updatedEducationHistory)) return this;

            return With(educationHistory: updatedEducationHistory);
        }
        public User RemoveEducation(Education educationToRemove)
        {
            var updatedEducationHistory = EducationHistory.RemoveEducationEntry(educationToRemove);

            if (EducationHistory.Equals(updatedEducationHistory)) return this;

            return With(educationHistory: updatedEducationHistory);
        }

        // Certificates
        public User ClearCertificates()
            => With(certificates: Certificates.Empty());

        public User ReplaceCertificates(Certificates newCertificates)
        {
            if (Certificates.Equals(newCertificates)) return this;

            return With(certificates: newCertificates);
        }
        public User AddCertificate(Certificate certificate)
        {
            var updatedCertificates = Certificates.AddCertificate(certificate: certificate);

            if (Certificates.Equals(updatedCertificates)) return this;

            return With(certificates: updatedCertificates);
        }
        public User UpdateCertificate(Certificate oldCertificate, Certificate newCertificate)
        {
            var updatedCertificates = Certificates.UpdateCertificate(oldCertificate: oldCertificate, newCertificate: newCertificate);

            if (Certificates.Equals(updatedCertificates)) return this;

            return With(certificates: updatedCertificates);
        }
        public User RemoveCertificate(Certificate certificateToRemove)
        {
            var updatedCertificates = Certificates.RemoveCertificate(certificateToRemove);

            if (Certificates.Equals(updatedCertificates)) return this;

            return With(certificates: updatedCertificates);
        }
    }
}
