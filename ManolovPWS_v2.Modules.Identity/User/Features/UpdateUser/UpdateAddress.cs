using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateAddressCommand(
        string Country,
        string Region,
        string Municipality,
        string City,
        string Street,
        string PostalCode)
        : ICommand<IdentityAppResult>;

    public sealed class UpdateAddressCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<UpdateAddressCommand, IdentityAppResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<IdentityAppResult> HandleAsync(UpdateAddressCommand command, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);

            var newAddress = Address.Create(
                command.Country, 
                command.Region, 
                command.Municipality, 
                command.City, 
                command.Street, 
                command.PostalCode);

            var updated = user.UpdateAddress(newAddress);

            var result = await _userRepository.SaveAsync(updated, cancellationToken);

            return (IdentityAppResult)(result.IsSuccess
                ? IdentityAppResults.Success()
                : IdentityAppResults.Failure(result.Errors!));
        }
    }
}
