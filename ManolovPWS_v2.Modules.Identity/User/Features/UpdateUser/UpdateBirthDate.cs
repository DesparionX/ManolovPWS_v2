using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateBirthDateCommand(DateOnly NewBirthDate) : ICommand<IdentityAppResult>;

    public sealed class UpdateBirthDateCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdateBirthDateCommand, IdentityAppResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<IdentityAppResult> HandleAsync(UpdateBirthDateCommand command, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);

            var newBirthDate = BirthDate.Create(command.NewBirthDate);

            var updated = user.UpdateBirthDate(newBirthDate);

            var result = await _userRepository.SaveAsync(updated, cancellationToken);

            return (IdentityAppResult)(result.IsSuccess
                ? IdentityAppResults.Success()
                : IdentityAppResults.Failure(result.Errors!));
        }
    }
}
