using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Exceptions;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Features.Shared.ReadModels;
using ManolovPWS_v2.Modules.Identity.User.Maps;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.GetUser
{
    public sealed record GetUserPublicProfileQuery(Guid UserId) : IQuery<PublicUserReadModel>;

    public sealed class GetUserPublicProfileQueryHandler(IUserRepository userRepository) 
        : IQueryHandler<GetUserPublicProfileQuery, PublicUserReadModel>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ITaskResult<PublicUserReadModel>> HandleAsync(GetUserPublicProfileQuery query, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(UserId.From(query.UserId.ToString()), cancellationToken)
                ?? throw new IdentityAppException("User cannot be null.", "InvalidUser");

            return IdentityAppResults.Success(user.ToPublicUserRm());
        }
    }
}
