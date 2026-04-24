using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Modules.Identity.User.Maps;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateCertificatesCommand(IEnumerable<Shared.SharedProperties.Certificate> NewCertificates) : ICommand<IdentityAppResult>;

    public sealed class UpdateCertificatesCommandHandler(IUserRepository userRepository, ICurrentUser<UserId> currentUser)
                : ICommandHandler<UpdateCertificatesCommand, IdentityAppResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<IdentityAppResult> HandleAsync(UpdateCertificatesCommand command, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FindByIdAsync(_currentUser.Id, cancellationToken);

            var newCertificates = command.NewCertificates.ToDomainCertificates();

            var updated = user.ReplaceCertificates(newCertificates);

            var result = await _userRepository.SaveAsync(updated, cancellationToken);

            return IdentityAppResults.FromResult(result);
        }
    }
}
