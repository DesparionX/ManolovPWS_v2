using ManolovPWS_v2.Domain.Contracts.Factories;
using ManolovPWS_v2.Domain.Models.Post;
using ManolovPWS_v2.Infrastructure.Contracts.Maps;
using ManolovPWS_v2.Infrastructure.Contracts.Results;
using ManolovPWS_v2.Infrastructure.Persistance;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Infrastructure.Contracts.Factories
{
    public sealed class PostFactory(AppDbContext context) : IPostFactory
    {
        private readonly AppDbContext _context = context;

        public async Task<ITaskResult<Post>> CreateAsync(Post entity, CancellationToken cancellationToken = default)
        {
            var dbEntity = entity.ToDbEntity();

            _context.Posts.Add(dbEntity);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0 ?
                Result<Post>.Success(dbEntity.ToDomain()) 
                : Result<Post>.Failure([new InfraError(Code: "PostCreationFailed", Message: "Failed to create the post.")]);
        }
    }
}
