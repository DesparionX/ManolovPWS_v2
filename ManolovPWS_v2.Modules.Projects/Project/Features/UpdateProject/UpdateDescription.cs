using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Projects.Project.Features.UpdateProject
{
    public sealed record UpdateProjectDescriptionCommand(string ProjectId, string NewDescription) : ICommand<ITaskResult>;

    public sealed class UpdateProjectDescriptionCommandHandler(IProjectRepository projectRepository)
        : ICommandHandler<UpdateProjectDescriptionCommand, ITaskResult>
    {
        private readonly IProjectRepository _repository = projectRepository;

        public async Task<ITaskResult> HandleAsync(UpdateProjectDescriptionCommand command, CancellationToken cancellationToken = default)
        {
            var newDescription = ProjectDescription.Create(command.NewDescription);

            var projectId = ProjectId.From(command.ProjectId);

            var result = await _repository.FindByIdAsync(projectId, cancellationToken);

            if (!result.IsSuccess)
                return Result.Failure(result.Errors);

            var project = result.Value;

            var updated = project.UpdateDescription(newDescription);

            var saveResult = await _repository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure(saveResult.Errors);
        }
    }
}
