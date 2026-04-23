using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using System.Reflection.Metadata.Ecma335;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdatePhoneNumberCommand(string NewPhoneNumber) : ICommand<IdentityAppResult>;

    public sealed class UpdatePhoneNumberCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdatePhoneNumberCommand, IdentityAppResult>
    {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<IdentityAppResult> HandleAsync(UpdatePhoneNumberCommand command, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);

            var newPhoneNumber = UserPhoneNumber.CreateOrNull(command.NewPhoneNumber);

            var updated = user.UpdatePhoneNumber(newPhoneNumber!);

            var result = await _userRepository.SaveAsync(updated, cancellationToken);

            return IdentityAppResults.FromResult(result);
        }
    }
}
