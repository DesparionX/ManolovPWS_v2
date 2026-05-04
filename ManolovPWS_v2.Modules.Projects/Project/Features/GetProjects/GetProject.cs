using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Project.Properties;
using ManolovPWS_v2.Modules.Projects.Project.Maps;
using ManolovPWS_v2.Modules.Projects.Project.Shared.ReadModels;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Projects.Project.Features.GetProjects
{
    public sealed record GetProjectQuery(string ProjectId) : IQuery<ProjectReadModel>;

    public sealed class GetProjectQueryHandler(IProjectRepository projectRepository)
        : IQueryHandler<GetProjectQuery, ProjectReadModel>
    {
        private readonly IProjectRepository _repository = projectRepository;
        public async Task<ITaskResult<ProjectReadModel>> HandleAsync(GetProjectQuery request, CancellationToken cancellationToken = default)
        {
            var projectId = ProjectId.From(request.ProjectId);

            var result = await _repository.FindByIdAsync(projectId, cancellationToken);

            return result.IsSuccess
                ? Result<ProjectReadModel>.Success(result.Value.ToReadModel())
                : Result<ProjectReadModel>.Failure(result.Errors);
        }
    }
}
