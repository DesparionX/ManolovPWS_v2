using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Modules.Content.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Content.Post.Features.DeletePost
{
    public sealed record RemovePostCommand(string PostId) : ICommand<ITaskResult>;

    public sealed class RemovePostCommandHandler(IPostRepository postRepository)
        : ICommandHandler<RemovePostCommand, ITaskResult>
    {
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<ITaskResult> HandleAsync(RemovePostCommand command, CancellationToken cancellationToken = default)
        {
            var postId = PostId.From(command.PostId);

            var result = await _postRepository.RemoveAsync(postId, cancellationToken);

            return result.IsSuccess
                ? Result.Success()
                : Result.Failure([ContentAppErrors.PostDeletionFailed]);
        }
    }
}
