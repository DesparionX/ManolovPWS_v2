using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Projects.Project.Features.UpdateProject
{
    public sealed record UpdateProjectGitHubUrl(string ProjectId, string NewGitHubUrl) : ICommand<ITaskResult>;

    public sealed class UpdateProjectGitHubUrlCommandHandler(IProjectRepository projectRepository)
        : ICommandHandler<UpdateProjectGitHubUrl, ITaskResult>
    {
        private readonly IProjectRepository _repository = projectRepository;

        public async Task<ITaskResult> HandleAsync(UpdateProjectGitHubUrl command, CancellationToken cancellationToken = default)
        {
            var newGitHubUrl = ProjectGitHubUrl.Create(command.NewGitHubUrl);

            var projectId = ProjectId.From(command.ProjectId);

            var result = await _repository.FindByIdAsync(projectId, cancellationToken);

            if (!result.IsSuccess)
                return Result.Failure(result.Errors);

            var project = result.Value;

            var updated = project.UpdateGitHubUrl(newGitHubUrl);

            var saveResult = await _repository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure(saveResult.Errors);
        }
    }
}
