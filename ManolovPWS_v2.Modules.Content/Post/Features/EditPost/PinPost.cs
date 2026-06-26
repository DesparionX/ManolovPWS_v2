using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Content.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;
using ManolovPWS_v2.Shared.Authorization;

namespace ManolovPWS_v2.Modules.Content.Post.Features.EditPost
{
    public sealed record PinPostCommand(string PostId) : ICommand;

    public sealed class PinPostCommandHandler(IPostRepository postRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<PinPostCommand>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<ITaskResult> HandleAsync(PinPostCommand command, CancellationToken cancellationToken = default)
        {
            var postId = PostId.From(command.PostId);

            var result = await _postRepository.FindByIdAsync(postId, cancellationToken);
            if (!result.IsSuccess)
                return Result.Failure([ContentAppErrors.PostNotFound]);

            var post = result.Value;
            if (!_currentUser.IsInRole(Roles.Owner))
                return Result.Failure([ContentAppErrors.Unauthorized]);

            var updatedPost = post.IsPinned ? post.UnpinPost() : post.PinPost();

            var saveResult = await _postRepository.SaveAsync(updatedPost, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure([ContentAppErrors.PostUpdateFailed]);
        }
    }
}
