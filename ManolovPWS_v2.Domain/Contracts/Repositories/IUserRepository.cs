using ManolovPWS_v2.Domain.Contracts.Results;
using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Domain.Models.User.Properties;

namespace ManolovPWS_v2.Domain.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<UserTaskResult> SaveUserAsync(User user);
        Task<UserTaskResult> RemoveUserAsync(User user);
        Task<UserTaskResult> FindUserById(UserId id);
        Task<UserTaskResult> FindUserByUserName(UserName userName);
        Task<UserTaskResult> FindUserByEmail(Email email);
    }
}
