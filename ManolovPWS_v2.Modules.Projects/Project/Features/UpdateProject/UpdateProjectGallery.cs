using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Modules.Projects.Project.Maps;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Projects.Project.Features.UpdateProject
{
    public sealed record UpdateProjectGalleryCommand(string ProjectId, IEnumerable<string> NewGallery) : ICommand<ITaskResult>;

    public sealed class UpdateProjectGalleryCommandHandler(IProjectRepository projectRepository)
        : ICommandHandler<UpdateProjectGalleryCommand, ITaskResult>
    {
        private readonly IProjectRepository _repository = projectRepository;

        public async Task<ITaskResult> HandleAsync(UpdateProjectGalleryCommand command, CancellationToken cancellationToken = default)
        {
            var projectId = ProjectId.From(command.ProjectId);

            var result = await _repository.FindByIdAsync(projectId, cancellationToken);

            if (!result.IsSuccess)
                return Result.Failure(result.Errors);

            var project = result.Value;

            var updated = project.ReplaceGallery(command.NewGallery.ToProjectPictures());

            var saveResult = await _repository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure(saveResult.Errors);
        }
    }
}
