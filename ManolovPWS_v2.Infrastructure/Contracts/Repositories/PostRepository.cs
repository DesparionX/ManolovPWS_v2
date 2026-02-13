using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Post;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Infrastructure.Contracts.Maps;
using ManolovPWS_v2.Infrastructure.Contracts.Results;
using ManolovPWS_v2.Infrastructure.Exceptions;
using ManolovPWS_v2.Infrastructure.Persistance;
using ManolovPWS_v2.Shared.Abstractions.Results;
using Microsoft.EntityFrameworkCore;

namespace ManolovPWS_v2.Infrastructure.Contracts.Repositories
{
    public sealed class PostRepository(AppDbContext context) : IPostRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IReadOnlyList<Post>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var posts = await _context.Posts.ToListAsync(cancellationToken);

            return posts.ToDomainList();
        }

        public async Task<Post> FindByIdAsync(PostId id, CancellationToken cancellationToken = default)
        {
            var post = await _context.Posts.FindAsync([id.Value], cancellationToken)
                ?? throw DbExceptions.PostNotFound(id.Value);

            return post.ToDomain();
        }

        public async Task<IReadOnlyList<Post>> FindByAuthorId(UserId authorId, CancellationToken cancellationToken = default)
        {
            var post = await _context.Posts.Where(p => p.AuthorId.Equals(authorId.Value)).ToListAsync(cancellationToken)
                ?? throw DbExceptions.PostNotFoundByAuthorId(authorId.Value);

            return post.ToDomainList();
        }

        public async Task<ITaskResult> RemoveAsync(PostId id, CancellationToken cancellationToken = default)
        {
            var postToRemove = await _context.Posts.FindAsync([id.Value], cancellationToken)
                ?? throw DbExceptions.PostNotFound(id.Value);

            _context.Posts.Remove(postToRemove);

            await _context.SaveChangesAsync(cancellationToken);

            return InfraTaskResults.Success();
        }

        public async Task<ITaskResult> SaveAsync(Post entity, CancellationToken cancellationToken = default)
        {
            var dbPost = entity.ToDbEntity();

            _context.Posts.Update(dbPost);

            await _context.SaveChangesAsync(cancellationToken);

            return InfraTaskResults.Success();
        }
    }
}
