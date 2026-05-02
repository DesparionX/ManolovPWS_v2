using ManolovPWS_v2.Domain.Contracts.Factories;
using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Domain.Models.Project.Properties.ProjectStack;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Projects.Project.Maps;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Projects.Project.Features.AddProject
{
    public sealed record AddNewProjectCommand(
        string Name,
        string Description,
        string ProjectState,
        string? LiveUrl,
        string? GitHubUrl,
        IEnumerable<string>? GalleryPictures,
        IEnumerable<string> ProjectStack,
        string ThumbUrl
    ) : ICommand<ITaskResult>;

    public sealed class AddNewProjectCommandHandler(IProjectFactory projectFactory, ICurrentUser<UserId> currentUser)
        : ICommandHandler<AddNewProjectCommand, ITaskResult>
    {
        private readonly IProjectFactory _projectFactory = projectFactory;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<ITaskResult> HandleAsync(AddNewProjectCommand command, CancellationToken cancellationToken = default)
        {
            var projectId = ProjectId.New();
            var projectName = ProjectName.Create(command.Name);
            var description = ProjectDescription.Create(command.Description);
            var projectState = ProjectState.FromString(command.ProjectState);
            var liveUrl = command.LiveUrl is not null ? ProjectLiveUrl.Create(command.LiveUrl) : null;
            var gitHubUrl = command.GitHubUrl is not null ? ProjectGitHubUrl.Create(command.GitHubUrl) : null;
            var galleryPictures = command.GalleryPictures is not null ? ProjectGallery.Create(command.GalleryPictures.ToProjectPictures()) : null;
            var projectStack = ProjectStack.Create(command.ProjectStack.ToProjectStack());
            var thumbUrl = ProjectPicture.Create(command.ThumbUrl);
            var uploadedDate = ProjectUploadedDate.Create(DateOnly.FromDateTime(DateTime.UtcNow));

            var newProject = Domain.Models.Project.Project.Create(
                id: projectId,
                ownerId: _currentUser.Id.Value,
                name: projectName,
                description: description,
                state: projectState,
                liveUrl: liveUrl,
                gitHubUrl: gitHubUrl,
                gallery: galleryPictures,
                stack: projectStack,
                thumb: thumbUrl,
                uploadedDate: uploadedDate
            );

            var result = await _projectFactory.CreateAsync(newProject, cancellationToken);

            return result.IsSuccess ?
                Result.Success()
                : Result.Failure(result.Errors);
        }
    }
}
