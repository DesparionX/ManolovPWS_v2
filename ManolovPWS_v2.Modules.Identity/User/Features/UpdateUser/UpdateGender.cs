using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateGenderCommand(string NewGender) : ICommand<IdentityAppResult>;

    public sealed class UpdateGenderCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdateGenderCommand, IdentityAppResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<IdentityAppResult> HandleAsync(UpdateGenderCommand command, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);
            
            var newGender = Gender.FromString(command.NewGender);
            
            var updated = user.ChangeGender(newGender);

            var result = await _userRepository.SaveAsync(updated, cancellationToken);

            return (IdentityAppResult)(result.IsSuccess
                ? IdentityAppResults.Success()
                : IdentityAppResults.Failure(result.Errors!));
        }
    }
}
