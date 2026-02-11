using ManolovPWS_v2.Domain.Models.Project;
using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;

namespace ManolovPWS_v2.Infrastructure.Contracts.Maps
{
    public static class ProjectExtensions
    {
        public static Project ToDomain(this DbProject dbProject)
        {
            return Project.Create(
                ProjectId.From(dbProject.Id.ToString()),
                dbProject.OwnerId,
                ProjectName.Create(dbProject.Name),
                ProjectDescription.Create(dbProject.Description),
                ProjectState.FromString(dbProject.ProjectState),
                ProjectPicture.Create(dbProject.Thumb),
                ProjectUploadedDate.Create(dbProject.UploadedDate),
                ProjectUpdatedDate.CreateOrNull(dbProject.UpdatedDate),
                ProjectLiveUrl.CreateOrNull(dbProject.LiveUrl),
                ProjectGitHubUrl.CreateOrNull(dbProject.GitHubUrl),
                ProjectGallery.Create(dbProject.Gallery?.Pictures)
            );
        }

        public static DbProject ToDbEntity(this Project project)
        {
            var dbProject = new DbProject
            {
                Id = project.Id.Value,
                OwnerId = project.OwnerId,
                Name = project.Name.Value,
                Description = project.Description.Value,
                ProjectState = project.State.Value.ToString(),
                LiveUrl = project.LiveUrl?.Value.ToString(),
                GitHubUrl = project.GitHubUrl?.Value.ToString(),
                UploadedDate = project.UploadedDate.Value,
                UpdatedDate = project.UpdatedDate?.Value,
                Gallery = project.Gallery,
                Thumb = project.Thumb.Value.ToString()
            };

            return dbProject;
        }

        public static IReadOnlyList<Project> ToDomainList(this IReadOnlyList<DbProject> projects)
            => projects.Select(p => p.ToDomain()).ToList();

        public static IReadOnlyList<DbProject> ToDbEntityList(this IReadOnlyList<Project> projects)
            => projects.Select(p => p.ToDbEntity()).ToList();
    }
}
