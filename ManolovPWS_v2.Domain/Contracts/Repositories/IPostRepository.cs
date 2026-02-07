using ManolovPWS_v2.Domain.Models.Post;
using ManolovPWS_v2.Domain.Models.Post.Properties;

namespace ManolovPWS_v2.Domain.Contracts.Repositories
{
    public interface IPostRepository : IRepository<Post, PostId>
    {
    }
}
