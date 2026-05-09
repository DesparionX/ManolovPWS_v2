using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Domain.Models.Post.Properties.PostContent;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Content.Post.Features.EditPost
{
    public sealed record EditPostContextCommand(string PostId, string NewContext) : ICommand<ITaskResult>;

    public sealed class EditPostContextCommandHandler(IPostRepository postRepository)
        : ICommandHandler<EditPostContextCommand, ITaskResult>
    {
        private readonly IPostRepository _postRepository = postRepository;
        public async Task<ITaskResult> HandleAsync(EditPostContextCommand command, CancellationToken cancellationToken = default)
        {
            var postId = PostId.From(command.PostId);
            var newContext = PostContext.Create(command.NewContext);

            var result = await _postRepository.FindByIdAsync(postId, cancellationToken);

            if (!result.IsSuccess)
                return Result.Failure(result.Errors);

            var post = result.Value;

            var updated = post.UpdateContext(newContext);

            var saveResult = await _postRepository.SaveAsync(updated, cancellationToken);

            return saveResult.IsSuccess
                ? Result.Success()
                : Result.Failure(saveResult.Errors);
        }
    }
}
