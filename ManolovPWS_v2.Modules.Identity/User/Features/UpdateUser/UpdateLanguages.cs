using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Maps;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateLanguageCommand(IEnumerable<Shared.SharedProperties.Language> NewLanguages) : ICommand<IdentityAppResult>;

    public sealed class UpdateLanguageCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
            : ICommandHandler<UpdateLanguageCommand, IdentityAppResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<IdentityAppResult> HandleAsync(UpdateLanguageCommand command, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);

            var newLanguages = command.NewLanguages.ToDomainLanguages();

            var updated = user.ReplaceLanguages(newLanguages);

            var result = await _userRepository.SaveAsync(updated, cancellationToken);

            return IdentityAppResults.FromResult(result);
        }
    }
}
