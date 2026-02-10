using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Domain.Models.User.Properties;

namespace ManolovPWS_v2.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User, UserId>
    {
        Task<User> FindByUserName(UserName userName, CancellationToken cancellationToken = default);
        Task<User> FindByEmail(Email email, CancellationToken cancellationToken = default);
    }
}
