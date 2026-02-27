using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Exceptions;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Maps;
using ManolovPWS_v2.Modules.Identity.User.Shared.ReadModels;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.GetUser
{
    public sealed record GetUserPrivateProfileQuery(string UserId) : IQuery<PrivateUserReadModel>;

    public sealed class GetUserPrivateProfileQueryHandler(IUserRepository userRepository)
        : IQueryHandler<GetUserPrivateProfileQuery, PrivateUserReadModel>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ITaskResult<PrivateUserReadModel>> HandleAsync(GetUserPrivateProfileQuery query, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(UserId.From(query.UserId), cancellationToken)
                ?? throw new IdentityAppException("User is null.", "InvalidUser");

            return IdentityAppResults.Success(user.ToPrivateUserRm());
        }
    }
}
