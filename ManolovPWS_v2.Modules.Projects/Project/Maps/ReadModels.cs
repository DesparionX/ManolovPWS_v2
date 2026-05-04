using ManolovPWS_v2.Modules.Projects.Project.Shared.ReadModels;

namespace ManolovPWS_v2.Modules.Projects.Project.Maps
{
    public static class ReadModels
    {
        public static ProjectReadModel ToReadModel(this Domain.Models.Project.Project project)
        {
            return new ProjectReadModel(
                Id: project.Id.Value.ToString(),
                OwnerId: project.OwnerId.ToString(),
                Name: project.Name.Value,
                Description: project.Description.Value,
                State: project.State.ToString(),
                LiveUrl: project.LiveUrl?.Value.ToString(),
                GitHubUrl: project.GitHubUrl?.Value.ToString(),
                UploadedDate: project.UploadedDate.Value,
                UpdatedDate: project.UpdatedDate?.Value,
                Gallery: project.Gallery.Pictures.Select(p => p.Value.ToString()).ToList(),
                Thumb: project.Thumb.Value.ToString(),
                Stack: project.ProjectStack.StackList.Select(t => t.Tag).ToList()
            );
        }

        public static IReadOnlyList<ProjectReadModel> ToReadModelList(this IReadOnlyList<Domain.Models.Project.Project> projects)
            => projects.Select(p => p.ToReadModel()).ToList();
    }
}
