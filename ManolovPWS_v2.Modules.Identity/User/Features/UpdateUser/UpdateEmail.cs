using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateEmailCommand(string NewEmail) : ICommand<ITaskResult>;

    public sealed class UpdateEmailCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdateEmailCommand, ITaskResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<ITaskResult> HandleAsync(UpdateEmailCommand command, CancellationToken cancellationToken = default)
        {
            var newEmail = Email.Create(command.NewEmail);

            var result = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);

            if (!result.IsSuccess)
                return Result.Failure(result.Errors);

            var user = result.Value;

            var updated = user.UpdateEmail(newEmail);
            
            var saveResult = await _userRepository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure(saveResult.Errors);
        }
    }
}
