using ManolovPWS_v2.Domain.Models.Post;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Domain.Contracts.Repositories
{
    public interface IPostRepository : IRepository<Post, PostId>
    {
        Task<ITaskResult<IReadOnlyList<Post>>> FindByAuthorId(UserId authorId, CancellationToken cancellationToken = default);
    }
}
