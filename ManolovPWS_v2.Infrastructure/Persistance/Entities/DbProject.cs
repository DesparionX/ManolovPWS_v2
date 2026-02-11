using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Domain.Models.Project.Properties;

namespace ManolovPWS_v2.Infrastructure.Persistance.Entities
{
    public sealed class DbProject : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProjectState { get; set; } = string.Empty;
        public string? LiveUrl { get; set; }
        public string? GitHubUrl { get; set; }
        public DateOnly UploadedDate { get; set; }
        public DateOnly? UpdatedDate { get; set; }
        public ProjectGallery? Gallery { get; set; }
        public string Thumb { get; set; } = string.Empty;
    }
}
