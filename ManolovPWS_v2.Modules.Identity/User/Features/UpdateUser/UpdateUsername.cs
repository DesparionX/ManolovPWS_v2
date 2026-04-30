using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateUsernameCommand(string NewUsername) : ICommand<ITaskResult>;
    
    public sealed class UpdateUsernameCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdateUsernameCommand, ITaskResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;
        public async Task<ITaskResult> HandleAsync(UpdateUsernameCommand command, CancellationToken cancellationToken = default)
        {
            var newUserName = UserName.Create(command.NewUsername);

            var result = await _userRepository.FindByIdAsync(UserId.From(_currentUser.Id.ToString()), cancellationToken);

            if (!result.IsSuccess)
                return Result.Failure(result.Errors);

            var user = result.Value;

            var updated = user.UpdateUserName(newUserName);

            var saveResult = await _userRepository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure(saveResult.Errors);
        }
    }
}
