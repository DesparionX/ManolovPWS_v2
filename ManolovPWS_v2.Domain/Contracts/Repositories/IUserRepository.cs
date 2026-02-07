using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User, UserId>
    {
        Task<ITaskResult<User>> FindByUserName(UserName userName);
        Task<ITaskResult<User>> FindByEmail(Email email);
    }
}
