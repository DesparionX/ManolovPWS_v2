using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Domain.Models.Post.Exceptions;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Domain.Models.Post.Properties.PostContent;

namespace ManolovPWS_v2.Domain.Models.Post
{
    public sealed class Post : IEntity<PostId>
    {
        public PostId Id { get; } 
        public Guid AuthorId { get; }
        public PostTitle Title { get; }
        public PostContent Content { get; }
        public PostPublishedDate PublishedDate { get; }
        public PostUpdatedDate? UpdatedDate { get; }
        public bool IsPinned { get; }

        private Post(
            PostId id,
            Guid authorId,
            PostTitle title,
            PostContent content,
            bool isPinned,
            PostPublishedDate publishedDate,
            PostUpdatedDate? updatedDate = default
            )
        {
            Id = id;
            AuthorId = authorId;
            Title = title;
            Content = content;
            PublishedDate = publishedDate;
            UpdatedDate = updatedDate;
            IsPinned = isPinned;
        }

        private Post With(
            PostTitle? title = default,
            PostContent? content = default,
            bool? isPinned = default
            )
        {
            var updateDate = PostUpdatedDate.Create(DateOnly.FromDateTime(DateTime.UtcNow));

            ValidateUpdateDate(PublishedDate, updateDate);
            
            return new(
                Id,
                AuthorId,
                title ?? Title,
                content ?? Content,
                isPinned ?? IsPinned,
                PublishedDate,
                updateDate
                );
        }

        public static Post Create(
            PostId id,
            Guid authorId,
            PostTitle title,
            PostContent content,
            bool isPinned,
            PostPublishedDate publishedDate,
            PostUpdatedDate? updatedDate = default
            )
        {
            ValidateUpdateDate(publishedDate, updatedDate);

            return new(id, authorId, title, content, isPinned, publishedDate, updatedDate);
        }

        // Post manipulations
        public Post UpdateTitle(PostTitle newTitle)
        {
            if (Title.Equals(newTitle)) return this;

            return With(title: newTitle);
        }
        public Post UpdateContent(PostContent newContent)
        {
            if (Content.Equals(newContent)) return this;

            return With(content: newContent);
        }
        public Post PinPost() => IsPinned ? this : With(isPinned: true);
        public Post UnpinPost() => !IsPinned ? this : With(isPinned: false);
        

        // Validations
        private static void ValidateUpdateDate(PostPublishedDate published, PostUpdatedDate? updated)
        {
            if (updated is not null && updated.Value < published.Value)
                throw new InvalidPostUpdatedDateException("Updated date cannot be earlier than uploaded date.");
        }
    }
}
