using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateUsernameCommand(string NewUsername) : ICommand<IdentityAppResult>;
    
    public sealed class UpdateUsernameCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdateUsernameCommand, IdentityAppResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;
        public async Task<IdentityAppResult> HandleAsync(UpdateUsernameCommand command, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(UserId.From(_currentUser.Id.ToString()), cancellationToken);
            
            var newUserName = UserName.Create(command.NewUsername);

            var updated = user.UpdateUserName(newUserName);

            var result = await _userRepository.SaveAsync(updated, cancellationToken);

            return IdentityAppResults.FromResult(result);
        }
    }
}
