using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.User.Maps;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateEducationHistoryCommand(IEnumerable<Shared.SharedProperties.Education> NewEducationHistory) : ICommand<ITaskResult>;

    public sealed class UpdateEducationHistoryCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
                : ICommandHandler<UpdateEducationHistoryCommand, ITaskResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<ITaskResult> HandleAsync(UpdateEducationHistoryCommand command, CancellationToken cancellationToken = default)
        {
            var newEducationHistory = command.NewEducationHistory.ToDomainEducation();

            var result = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);

            if (!result.IsSuccess)
                return Result.Failure(result.Errors);

            var user = result.Value;

            var updated = user.ReplaceEducation(newEducationHistory);

            var saveResult = await _userRepository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure(saveResult.Errors);
        }
    }
}
