using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Maps;
using ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.GetUser
{
    public sealed record GetAllUsersQuery() : IQuery<IReadOnlyList<CompactUserReadModel>>;

    public sealed class GetAllUsersQueryHandler(IUserRepository userRepository)
        : IQueryHandler<GetAllUsersQuery, IReadOnlyList<CompactUserReadModel>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ITaskResult<IReadOnlyList<CompactUserReadModel>>> HandleAsync(GetAllUsersQuery request, CancellationToken cancellationToken = default)
        {
            var result = await _userRepository.GetAllAsync(cancellationToken);
            if (!result.IsSuccess)
                return Result<IReadOnlyList<CompactUserReadModel>>.Failure([IdentityAppErrors.UsersRetrievalFailed, ..result.Errors]);

            var users = result.Value.Select(u => u.ToCompactUserRm()).ToList();

            return Result<IReadOnlyList<CompactUserReadModel>>.Success(users);
        }
    }
}
