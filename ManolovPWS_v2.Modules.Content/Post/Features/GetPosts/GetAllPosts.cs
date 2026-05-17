using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Modules.Content.Post.Maps;
using ManolovPWS_v2.Modules.Content.Post.Shared.ReadModels;
using ManolovPWS_v2.Modules.Content.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Content.Post.Features.GetPosts
{
    public sealed record GetAllPostsQuery() : IQuery<IEnumerable<PostReadModel>>;

    public sealed class GetAllPostsQueryHandler(IPostRepository postRepository) 
        : IQueryHandler<GetAllPostsQuery, IEnumerable<PostReadModel>>
    {
        private readonly IPostRepository _postRepository = postRepository;
        public async Task<ITaskResult<IEnumerable<PostReadModel>>> HandleAsync(GetAllPostsQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _postRepository.GetAllAsync(cancellationToken);

            if (!result.IsSuccess)
                return Result<IEnumerable<PostReadModel>>.Failure([ContentAppErrors.NoPostsFound]);

            var posts = result.Value.Select(p => p.ToReadModel());

            return Result<IEnumerable<PostReadModel>>.Success(posts);
        }
    }
}
