using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateAddressCommand(
        string Country,
        string Region,
        string Municipality,
        string City,
        string Street,
        string PostalCode)
        : ICommand<ITaskResult>;

    public sealed class UpdateAddressCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdateAddressCommand, ITaskResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<ITaskResult> HandleAsync(UpdateAddressCommand command, CancellationToken cancellationToken = default)
        {
            var newAddress = Address.Create(
                command.Country,
                command.Region,
                command.Municipality,
                command.City,
                command.Street,
                command.PostalCode);

            var result = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);

            if(!result.IsSuccess)
                return Result.Failure(result.Errors!);

            var user = result.Value;

            var updated = user.UpdateAddress(newAddress);

            var saveResult = await _userRepository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure(saveResult.Errors!);
        }
    }
}
