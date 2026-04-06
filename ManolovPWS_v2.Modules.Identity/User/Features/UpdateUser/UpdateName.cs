using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateNameCommand(string FirstName, string LastName, string? MiddleName = default)
        : ICommand<IdentityAppResult>;

    public sealed class UpdateUserNameCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdateNameCommand, IdentityAppResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<IdentityAppResult> HandleAsync(UpdateNameCommand command, CancellationToken cancellationToken = default)
        {
            var newName = Name.Create(
                firstName: command.FirstName,
                middleName: command.MiddleName,
                lastName: command.LastName);

            var user = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);

            var updated = user.UpdateName(newName);

            var result = await _userRepository.SaveAsync(updated, cancellationToken);

            return (IdentityAppResult)(result.IsSuccess
                ? IdentityAppResults.Success()
                : IdentityAppResults.Failure(result.Errors!));
        }
    }
}
