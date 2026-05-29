using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Modules.Projects.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Projects.Project.Features.UpdateProject
{
    public sealed record UpdateProjectState(string ProjectId, string NewState) : ICommand;

    public sealed class UpdateProjectStateCommandHandler(IProjectRepository projectRepository)
        : ICommandHandler<UpdateProjectState>
    {
        private readonly IProjectRepository _repository = projectRepository;

        public async Task<ITaskResult> HandleAsync(UpdateProjectState command, CancellationToken cancellationToken = default)
        {
            var newState = ProjectState.FromString(command.NewState);

            var projectId = ProjectId.From(command.ProjectId);

            var result = await _repository.FindByIdAsync(projectId, cancellationToken);

            if (!result.IsSuccess)
                return Result.Failure([ProjectAppErrors.ProjectNotFound]);

            var project = result.Value;

            var updated = project.UpdateState(newState);

            var saveResult = await _repository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure([ProjectAppErrors.ProjectUpdateFailed]);
        }
    }
}
