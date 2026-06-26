using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Domain.Models.Post.Properties.PostContent;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Content.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Content.Post.Features.EditPost
{
    public sealed record EditPostContextCommand(string PostId, string NewContext) : ICommand;

    public sealed class EditPostContextCommandHandler(IPostRepository postRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<EditPostContextCommand>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<ITaskResult> HandleAsync(EditPostContextCommand command, CancellationToken cancellationToken = default)
        {
            var postId = PostId.From(command.PostId);
            var newContext = PostContext.Create(command.NewContext);

            var result = await _postRepository.FindByIdAsync(postId, cancellationToken);
            if (!result.IsSuccess)
                return Result.Failure([ContentAppErrors.PostNotFound]);

            var post = result.Value;
            if (!post.AuthorId.Equals(_currentUser.Id.Value))
                return Result.Failure([ContentAppErrors.Unauthorized]);
        

            var updated = post.UpdateContext(newContext);

            var saveResult = await _postRepository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure([ContentAppErrors.PostUpdateFailed]);
        }
    }
}
