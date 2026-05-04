using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Projects.Exceptions;
using ManolovPWS_v2.Modules.Projects.Project.Maps;
using ManolovPWS_v2.Modules.Projects.Project.Shared.ReadModels;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Projects.Project.Features.GetProjects
{
    public sealed record GetUserProjectsQuery(string UserId) : IQuery<IReadOnlyList<ProjectReadModel>>;

    public sealed class GetUserProjectsQueryHandler(IProjectRepository projectRepository)
        : IQueryHandler<GetUserProjectsQuery, IReadOnlyList<ProjectReadModel>>
    {
        private readonly IProjectRepository _repository = projectRepository;
        
        public async Task<ITaskResult<IReadOnlyList<ProjectReadModel>>> HandleAsync(GetUserProjectsQuery request, CancellationToken cancellationToken = default)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.UserId))
                throw new ProjectsAppException("Request is null or UserId is invalid.", "InvalidRequest");
            var userId = UserId.From(request.UserId);

            var result = await _repository.FindByOwner(userId, cancellationToken);

            return result.IsSuccess
                ? Result<IReadOnlyList<ProjectReadModel>>.Success(result.Value.ToReadModelList())
                : Result<IReadOnlyList<ProjectReadModel>>.Failure(result.Errors);
        }
    }
}
