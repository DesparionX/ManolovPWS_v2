using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Domain.Models.User.Properties.Certificates;
using ManolovPWS_v2.Domain.Models.User.Properties.Education;
using ManolovPWS_v2.Domain.Models.User.Properties.Experience;
using ManolovPWS_v2.Domain.Models.User.Properties.SkillSet;
using Microsoft.AspNetCore.Identity;

namespace ManolovPWS_v2.Infrastructure.Persistance.Entities
{
    public sealed class DbUser : IdentityUser<Guid>, IEntity<Guid>
    {
        public required string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public required string LastName { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public SkillSet? Skills { get; set; }
        public Experience? Experience { get; set; }
        public EducationHistory? EducationHistory { get; set; }
        public Certificates? Certificates { get; set; }

    }
}
