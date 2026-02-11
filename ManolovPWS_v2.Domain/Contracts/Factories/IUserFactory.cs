using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Domain.Models.User.Properties;

namespace ManolovPWS_v2.Domain.Contracts.Factories
{
    public interface IUserFactory : IFactory<User, UserId>
    {
        public Task<User?> CreateWithPasswordAsync(User entity, string password, CancellationToken cancellationToken = default);
    }
}
