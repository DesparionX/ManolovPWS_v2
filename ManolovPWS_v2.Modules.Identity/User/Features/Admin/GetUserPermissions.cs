using ManolovPWS_v2.Modules.Identity.User.Auth.Authorization;
using ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.Admin
{
    public sealed record GetUserPermissionsQuery(string UserId) : IQuery<UserPermissionsReadModel>;

    public sealed class GetUserPermissionsQueryHandler(
        IAuthorizationService authorizationService)
        : IQueryHandler<GetUserPermissionsQuery, UserPermissionsReadModel>
    {
        private readonly IAuthorizationService _authorizationService = authorizationService;

        public async Task<ITaskResult<UserPermissionsReadModel>> HandleAsync(GetUserPermissionsQuery query, CancellationToken cancellationToken = default)
        {
            var roles = await _authorizationService.GetUserPermissionsAsync(query.UserId, cancellationToken);

            var rm = new UserPermissionsReadModel(
                UserId: query.UserId,
                Permissions: roles?.ToList() ?? []
                );

            return Result<UserPermissionsReadModel>.Success(rm);
        }
    }
}
