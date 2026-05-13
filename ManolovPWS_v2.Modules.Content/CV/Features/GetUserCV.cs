using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Modules.Content.CV.Services;
using ManolovPWS_v2.Modules.Content.CV.Shared.ReadModels;
using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;
using ManolovPWS_v2.Shared.Authorization;

namespace ManolovPWS_v2.Modules.Content.CV.Features
{
    public sealed record GetUserCVQuery() : IQuery<PublicCVReadModel>;

    public sealed class GetUserCVQueryHandler(
        IAuthorizationService authorizationService,
        IProjectRepository projectRepository,
        ICVBuilder cvBuilder)
        : IQueryHandler<GetUserCVQuery, PublicCVReadModel>
    {
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly ICVBuilder _cvBuilder = cvBuilder;

        public async Task<ITaskResult<PublicCVReadModel>> HandleAsync (GetUserCVQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _authorizationService.GetOwnerAsync(Roles.Owner, cancellationToken);
            if (!result.IsSuccess)
                return Result<PublicCVReadModel>.Failure(result.Errors);

            var owner = result.Value;

            var projectsRes = await _projectRepository.FindByOwnerIdAsync(owner.Id, cancellationToken);
            if (!projectsRes.IsSuccess)
                return Result<PublicCVReadModel>.Failure(projectsRes.Errors);

            var cv = _cvBuilder.Build(user: owner, projects: projectsRes.Value);

            return Result<PublicCVReadModel>.Success(cv);
        }
    }
}
