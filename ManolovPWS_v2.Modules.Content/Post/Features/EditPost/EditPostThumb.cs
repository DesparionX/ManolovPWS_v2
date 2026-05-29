using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Domain.Models.Post.Properties.PostContent;
using ManolovPWS_v2.Modules.Content.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Content.Post.Features.EditPost
{
    public sealed record EditPostThumbCommand(string PostId, string NewThumb) : ICommand;

    public sealed class EditPostThumbCommandHandler(IPostRepository postRepository)
        : ICommandHandler<EditPostThumbCommand>
    {
        private readonly IPostRepository _postRepository = postRepository;
        public async Task<ITaskResult> HandleAsync(EditPostThumbCommand command, CancellationToken cancellationToken = default)
        {
            var postId = PostId.From(command.PostId);

            var newThumb = PostPicture.Create(command.NewThumb);

            var result = await _postRepository.FindByIdAsync(postId, cancellationToken);

            if (!result.IsSuccess)
                return Result.Failure([ContentAppErrors.PostNotFound]);

            var post = result.Value;

            var updated = post.UpdateThumb(newThumb);

            var saveResult = await _postRepository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure([ContentAppErrors.PostUpdateFailed]);
        }
    }
}
