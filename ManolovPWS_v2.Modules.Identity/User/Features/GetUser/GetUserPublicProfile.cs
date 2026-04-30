using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Maps;
using ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.GetUser
{
    public sealed record GetUserPublicProfileQuery(string UserId) : IQuery<PublicUserReadModel>;

    public sealed class GetUserPublicProfileQueryHandler(IUserRepository userRepository) 
        : IQueryHandler<GetUserPublicProfileQuery, PublicUserReadModel>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ITaskResult<PublicUserReadModel>> HandleAsync(GetUserPublicProfileQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _userRepository.FindByIdAsync(UserId.From(query.UserId), cancellationToken);

            return result.IsSuccess
                ? Result<PublicUserReadModel>.Success(result.Value.ToPublicUserRm())
                : Result<PublicUserReadModel>.Failure([new IdentityAppError(Code: "InvalidUser", Message: "User not found.")]);
        }
    }
}
