using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;

namespace ManolovPWS_v2.Modules.Identity.User.Features.UpdateUser
{
    public sealed record UpdateUserProfileCommand(string FirstName, string LastName, string? MiddleName = default)
        : ICommand<IdentityAppResult>;
}
