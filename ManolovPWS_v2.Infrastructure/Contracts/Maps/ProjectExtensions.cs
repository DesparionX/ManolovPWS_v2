using ManolovPWS_v2.Domain.Models.Project;
using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Domain.Models.Project.Properties.ProjectStack;
using ManolovPWS_v2.Infrastructure.Exceptions;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;

namespace ManolovPWS_v2.Infrastructure.Contracts.Maps
{
    public static class ProjectExtensions
    {
        public static Project ToDomain(this DbProject dbProject)
        {
            if (dbProject is null)
                throw new InfrastructureException("DbProject cannot be null.", "NullDbProjectException");

            return Project.Create(
                id: ProjectId.From(dbProject.Id.ToString()),
                ownerId: dbProject.OwnerId,
                name: ProjectName.Create(dbProject.Name),
                description: ProjectDescription.Create(dbProject.Description),
                state: ProjectState.FromString(dbProject.ProjectState),
                thumb: ProjectPicture.Create(dbProject.Thumb),
                stack: ProjectStack.Create(dbProject.Stack?.StackList),
                uploadedDate: ProjectUploadedDate.Create(dbProject.UploadedDate),
                updatedDate: ProjectUpdatedDate.CreateOrNull(dbProject.UpdatedDate),
                liveUrl: ProjectLiveUrl.CreateOrNull(dbProject.LiveUrl),
                gitHubUrl: ProjectGitHubUrl.CreateOrNull(dbProject.GitHubUrl),
                gallery: ProjectGallery.Create(dbProject.Gallery?.Pictures)
            );
        }

        public static DbProject ToDbEntity(this Project project)
        {
            if (project is null)
                throw new InfrastructureException("Project cannot be null.", "NullProjectException");

            return new DbProject
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
                Thumb = project.Thumb.Value.ToString(),
                Stack = project.ProjectStack
            };
        }

        public static IReadOnlyList<Project> ToDomainList(this IReadOnlyList<DbProject> projects)
            => projects.Select(p => p.ToDomain()).ToList();

        public static IReadOnlyList<DbProject> ToDbEntityList(this IReadOnlyList<Project> projects)
            => projects.Select(p => p.ToDbEntity()).ToList();
    }
}
