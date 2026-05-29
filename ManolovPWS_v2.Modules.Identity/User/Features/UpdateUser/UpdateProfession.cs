using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateProfessionCommand(string Profession) : ICommand;
    
    public sealed class UpdateProfessionCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdateProfessionCommand>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<ITaskResult> HandleAsync(UpdateProfessionCommand command, CancellationToken cancellationToken = default)
        {
            var newProfession = Profession.Create(command.Profession);

            var result = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);

            if (!result.IsSuccess)
                return Result.Failure([IdentityAppErrors.UserNotFound]);

            var user = result.Value;

            var updated = user.UpdateProfession(newProfession);

            var saveResult = await _userRepository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure([IdentityAppErrors.UserUpdateFailed]);
        }
    }

}
