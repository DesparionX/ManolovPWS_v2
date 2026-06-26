using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Content.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;
using ManolovPWS_v2.Shared.Authorization;

namespace ManolovPWS_v2.Modules.Content.Post.Features.DeletePost
{
    public sealed record RemovePostCommand(string PostId) : ICommand;

    public sealed class RemovePostCommandHandler(IPostRepository postRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<RemovePostCommand>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<ITaskResult> HandleAsync(RemovePostCommand command, CancellationToken cancellationToken = default)
        {
            var postId = PostId.From(command.PostId);

            var postResult = await _postRepository.FindByIdAsync(postId, cancellationToken);
            if (!postResult.IsSuccess)
                return Result.Failure([ContentAppErrors.PostNotFound, ..postResult.Errors]);

            var authorId = postResult.Value.AuthorId;
            var isPostAuthor = _currentUser.Id.Equals(authorId);
            var isAppOwner = _currentUser.IsInRole(Roles.Owner);
            
            if (!isPostAuthor && !isAppOwner)
                return Result.Failure([ContentAppErrors.Unauthorized]);

            var result = await _postRepository.RemoveAsync(postId, cancellationToken);

            return result.IsSuccess
                ? Result.Success()
                : Result.Failure([ContentAppErrors.PostDeletionFailed, ..result.Errors]);
        }
    }
}
