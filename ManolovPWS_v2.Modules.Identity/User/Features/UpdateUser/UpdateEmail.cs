using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateEmailCommand(string NewEmail) : ICommand<IdentityAppResult>;

    public sealed class UpdateEmailCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdateEmailCommand, IdentityAppResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<IdentityAppResult> HandleAsync(UpdateEmailCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);
            var newEmail = Email.Create(command.NewEmail);

            var updated = user.UpdateEmail(newEmail);
            
            var result = await _userRepository.SaveAsync(updated, cancellationToken);

            return (IdentityAppResult)(result.IsSuccess
                ? IdentityAppResults.Success()
                : IdentityAppResults.Failure(result.Errors!));
        }
    }
}
