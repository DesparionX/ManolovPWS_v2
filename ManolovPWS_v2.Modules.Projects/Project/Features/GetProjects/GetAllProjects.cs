using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Modules.Projects.Project.Shared.ReadModels;
using ManolovPWS_v2.Modules.Projects.Project.Maps;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Projects.Project.Features.GetProjects
{
    public sealed record GetAllProjectsQuery() : IQuery<IReadOnlyList<ProjectReadModel>>;

    public sealed class GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        : IQueryHandler<GetAllProjectsQuery, IReadOnlyList<ProjectReadModel>>
    {
        private readonly IProjectRepository projectRepository = projectRepository;

        public async Task<ITaskResult<IReadOnlyList<ProjectReadModel>>> HandleAsync(GetAllProjectsQuery query, CancellationToken cancellationToken = default)
        {
            var result = await projectRepository.GetAllAsync(cancellationToken);

            return result.IsSuccess
                ? Result<IReadOnlyList<ProjectReadModel>>.Success(result.Value.ToReadModelList())
                : Result<IReadOnlyList<ProjectReadModel>>.Failure(result.Errors);
        }
    }
}
