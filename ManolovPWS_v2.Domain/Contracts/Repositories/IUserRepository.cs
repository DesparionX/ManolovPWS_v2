using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User, UserId>
    {
        Task<ITaskResult<User>> FindByUserNameAsync(UserName userName, CancellationToken cancellationToken = default);
        Task<ITaskResult<User>> FindByEmailAsync(Email email, CancellationToken cancellationToken = default);
        Task<bool> EmailExistsAsync(Email email, CancellationToken cancellationToken = default);
        Task<bool> UserNameExistsAsync(UserName userName, CancellationToken cancellationToken = default);
    }
}
