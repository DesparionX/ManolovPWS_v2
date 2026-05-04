using ManolovPWS_v2.Domain.Contracts.Factories;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Domain.Models.Post.Properties.PostContent;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Content.Post.Maps;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Identity;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Content.Post.Features.AddPost
{
    public sealed record AddNewPostCommand(
        string Title,
        string Context,
        string? Thumb,
        IEnumerable<string>? Gallery,
        bool IsPinned
        )
        : ICommand<ITaskResult>;

    public sealed class AddNewPostCommandHandler(IPostFactory postFactory, ICurrentUser<UserId> currentUser)
        : ICommandHandler<AddNewPostCommand, ITaskResult>
    {
        private readonly IPostFactory _postFactory = postFactory;
        private readonly ICurrentUser<UserId> _currentUser = currentUser;

        public async Task<ITaskResult> HandleAsync(AddNewPostCommand command, CancellationToken cancellationToken = default)
        {
            var postId = PostId.New();
            var title = PostTitle.Create(command.Title);
            
            var context = PostContext.Create(command.Context);
            var thumb = command.Thumb is not null ? PostPicture.Create(command.Thumb) : null;
            var gallery = command.Gallery is not null ? PostGallery.Create(command.Gallery.ToPostPictures()) : null;

            var content = PostContent.Create(context, thumb, gallery);

            var publishedAt = PostPublishedDate.Create(DateOnly.FromDateTime(DateTime.UtcNow));

            var newPost = Domain.Models.Post.Post.Create(
                id: postId,
                authorId: _currentUser.Id.Value,
                title: title,
                content: content,
                publishedDate: publishedAt,
                isPinned: command.IsPinned
                );

            var result = await _postFactory.CreateAsync(newPost, cancellationToken);

            return result.IsSuccess
                ? Result.Success()
                : Result.Failure(result.Errors);
        }
    }
}
