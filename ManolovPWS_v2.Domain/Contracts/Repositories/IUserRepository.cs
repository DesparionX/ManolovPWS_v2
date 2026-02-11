using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Domain.Models.User.Properties;

namespace ManolovPWS_v2.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User, UserId>
    {
        Task<User> FindByUserNameAsync(UserName userName, CancellationToken cancellationToken = default);
        Task<User> FindByEmailAsync(Email email, CancellationToken cancellationToken = default);
        Task<bool> EmailExistsAsync(Email email, CancellationToken cancellationToken = default);
        Task<bool> UserNameExistsAsync(UserName userName, CancellationToken cancellationToken = default);
    }
}
