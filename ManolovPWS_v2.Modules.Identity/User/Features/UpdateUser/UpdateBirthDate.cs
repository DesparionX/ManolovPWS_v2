using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateBirthDateCommand(DateOnly NewBirthDate) : ICommand<ITaskResult>;

    public sealed class UpdateBirthDateCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdateBirthDateCommand, ITaskResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<ITaskResult> HandleAsync(UpdateBirthDateCommand command, CancellationToken cancellationToken = default)
        {
            var newBirthDate = BirthDate.Create(command.NewBirthDate);

            var result = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);

            if (!result.IsSuccess)
                return Result.Failure(result.Errors);

            var user = result.Value;

            var updated = user.UpdateBirthDate(newBirthDate);

            var saveResult = await _userRepository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure(saveResult.Errors);
        }
    }
}
