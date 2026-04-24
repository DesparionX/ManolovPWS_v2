using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Maps;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateExperienceCommand(IEnumerable<Shared.SharedProperties.Job> Experience) : ICommand<IdentityAppResult>;

    public sealed class UpdateExperienceCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdateExperienceCommand, IdentityAppResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<IdentityAppResult> HandleAsync(UpdateExperienceCommand command, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(UserId.From(_currentUser.Id.ToString()), cancellationToken);

            var newExperience = command.Experience.ToDomainExperience();

            var updated = user.ReplaceExperience(newExperience);
        }
    }
}
