using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdatePhoneNumberCommand(string NewPhoneNumber) : ICommand;

    public sealed class UpdatePhoneNumberCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdatePhoneNumberCommand>
    {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<ITaskResult> HandleAsync(UpdatePhoneNumberCommand command, CancellationToken cancellationToken = default)
        {
            var newPhoneNumber = UserPhoneNumber.CreateOrNull(command.NewPhoneNumber);

            var result = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);
            
            if (!result.IsSuccess)
                return Result.Failure([IdentityAppErrors.UserNotFound]);

            var user = result.Value;

            var updated = user.UpdatePhoneNumber(newPhoneNumber!);

            var saveResult = await _userRepository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure([IdentityAppErrors.UserUpdateFailed]);
        }
    }
}
