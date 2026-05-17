using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Modules.Projects.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Projects.Project.Features.DeleteProject
{
    public sealed record RemoveProjectCommand(string ProjectId) : ICommand<ITaskResult>;

    public sealed class RemoveProjectCommandHandler(IProjectRepository projectRepository)
        : ICommandHandler<RemoveProjectCommand, ITaskResult>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<ITaskResult> HandleAsync(RemoveProjectCommand command, CancellationToken cancellationToken = default)
        {
            var projectId = ProjectId.From(command.ProjectId);

            var result = await _projectRepository.RemoveAsync(projectId, cancellationToken);

            return result.IsSuccess
                    ? Result.Success()
                    : Result.Failure([ProjectAppErrors.ProjectDeletionFailed]);
        }
    }
}
