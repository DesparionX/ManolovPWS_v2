using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Content.Post.Maps;
using ManolovPWS_v2.Modules.Content.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Content.Post.Features.EditPost
{
    public sealed record EditPostGalleryCommand(string PostId, IEnumerable<string> NewGallery) : ICommand;

    public sealed class EditPostGalleryCommandHandler(IPostRepository postRepository, ICurrentUser<UserId> currentUser)
        : ICommandHandler<EditPostGalleryCommand>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<ITaskResult> HandleAsync(EditPostGalleryCommand command, CancellationToken cancellationToken = default)
        {
            var postId = PostId.From(command.PostId);
            var newGallery = command.NewGallery.ToPostPictures();

            var result = await _postRepository.FindByIdAsync(postId, cancellationToken);
            if (!result.IsSuccess)
                return Result.Failure([ContentAppErrors.PostNotFound]);

            var post = result.Value;
            if (!post.AuthorId.Equals(_currentUser.Id.Value))
                return Result.Failure([ContentAppErrors.Unauthorized]);

            var updated = post.ReplaceGallery(newGallery);
            var saveResult = await _postRepository.SaveAsync(updated, cancellationToken);
            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure([ContentAppErrors.PostUpdateFailed]);
        }
    }
}
