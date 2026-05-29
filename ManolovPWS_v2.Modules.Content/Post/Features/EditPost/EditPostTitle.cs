using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Modules.Content.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Content.Post.Features.EditPost
{
    public sealed record EditPostTitleCommand(string PostId, string NewTitle) : ICommand;

    public sealed class EditPostTitleCommandHandler(IPostRepository postRepository)
        : ICommandHandler<EditPostTitleCommand>
    {
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<ITaskResult> HandleAsync(EditPostTitleCommand command, CancellationToken cancellationToken = default)
        {
            var postId = PostId.From(command.PostId);
            var newTitle = PostTitle.Create(command.NewTitle);

            var result = await _postRepository.FindByIdAsync(postId, cancellationToken);

            if (!result.IsSuccess)
                return Result.Failure([ContentAppErrors.PostNotFound]);

            var post = result.Value;

            var updated = post.UpdateTitle(newTitle);

            var saveResult = await _postRepository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure([ContentAppErrors.PostUpdateFailed]);
        }
    }
}
