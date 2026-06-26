using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Modules.Content.Post.Maps;
using ManolovPWS_v2.Modules.Content.Post.Shared.ReadModels;
using ManolovPWS_v2.Modules.Content.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Content.Post.Features.GetPosts
{
    public sealed record GetPostByIdQuery(string Id) : IQuery<PostReadModel>;

    public sealed class GetPostByIdQueryHandler(IPostRepository postRepository)
        : IQueryHandler<GetPostByIdQuery, PostReadModel>
    {
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<ITaskResult<PostReadModel>> HandleAsync(GetPostByIdQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _postRepository.FindByIdAsync(PostId.From(query.Id), cancellationToken);

            if (!result.IsSuccess)
                return Result<PostReadModel>.Failure([ContentAppErrors.PostNotFound, ..result.Errors]);

            var post = result.Value.ToReadModel();

            return Result<PostReadModel>.Success(post);
        }
    }
}
