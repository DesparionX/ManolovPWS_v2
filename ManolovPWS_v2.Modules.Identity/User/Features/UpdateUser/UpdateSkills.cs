using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Domain.Models.User.Properties.SkillSet;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Maps;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateSkillsCommand(IEnumerable<Shared.SharedProperties.Skill> Skills) : ICommand<IdentityAppResult>;

    public sealed class UpdateSkillsCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdateSkillsCommand, IdentityAppResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<IdentityAppResult> HandleAsync(UpdateSkillsCommand command, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);

            var newSkills = command.Skills.ToDomainSkills();

            var updated = user.ReplaceSkills(newSkills);

            var result = await _userRepository.SaveAsync(updated, cancellationToken);

            return IdentityAppResults.FromResult(result);
        }
    }
}
