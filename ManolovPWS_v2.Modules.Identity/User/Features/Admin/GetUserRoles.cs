using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
using ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.Admin
{
    public sealed record GetUserRolesQuery(string UserId) : IQuery<UserRolesReadModel>;

    public sealed class GetUserRolesQueryHandler(
        IAuthorizationService authorizationService)
        : IQueryHandler<GetUserRolesQuery, UserRolesReadModel>
    {
        private readonly IAuthorizationService _authorizationService = authorizationService;

        public async Task<ITaskResult<UserRolesReadModel>> HandleAsync(GetUserRolesQuery query, CancellationToken cancellationToken = default)
        {
            var roles = await _authorizationService.GetUserRolesAsync(query.UserId, cancellationToken);

            var rm = new UserRolesReadModel(
                UserId: query.UserId,
                Roles: roles?.ToList() ?? []
                );

            return Result<UserRolesReadModel>.Success(rm);
        }
    }
}
