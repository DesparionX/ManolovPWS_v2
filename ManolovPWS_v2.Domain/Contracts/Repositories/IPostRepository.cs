using ManolovPWS_v2.Domain.Models.Post;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Domain.Models.Project;
using ManolovPWS_v2.Domain.Models.User.Properties;

namespace ManolovPWS_v2.Domain.Contracts.Repositories
{
    public interface IPostRepository : IRepository<Post, PostId>
    {
        Task<IReadOnlyList<Post>> FindByAuthorId(UserId authorId, CancellationToken cancellationToken = default);
    }
}
