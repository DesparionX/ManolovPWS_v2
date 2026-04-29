using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Auth.Authentication
{
    public interface IAuthService
    {
        public Task<ITaskResult<Domain.Models.User.User>> AuthenticateAsync(string emailOrUserName, string password, CancellationToken cancellationToken = default);
    }
}
