using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Modules.Content.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Content.Post.Features.EditPost
{
    public sealed record PinPostCommand(string PostId) : ICommand;

    public sealed class PinPostCommandHandler(IPostRepository postRepository)
        : ICommandHandler<PinPostCommand>
    {
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<ITaskResult> HandleAsync(PinPostCommand command, CancellationToken cancellationToken = default)
        {
            var postId = PostId.From(command.PostId);
            var result = await _postRepository.FindByIdAsync(postId, cancellationToken);

            if (!result.IsSuccess)
                return Result.Failure([ContentAppErrors.PostNotFound]);

            var post = result.Value;

            var updatedPost = post.IsPinned ? post.UnpinPost() : post.PinPost();

            var saveResult = await _postRepository.SaveAsync(updatedPost, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure([ContentAppErrors.PostUpdateFailed]);
        }
    }
}
