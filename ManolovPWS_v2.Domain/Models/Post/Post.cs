using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Domain.Models.Post.Exceptions;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Domain.Models.Post.Properties.PostContent;
using ManolovPWS_v2.Domain.Models.Project.Exceptions;
using ManolovPWS_v2.Domain.Models.Project.Properties;

namespace ManolovPWS_v2.Domain.Models.Post
{
    public sealed class Post : IEntity<PostId>
    {
        public PostId? Id { get; } 
        public Guid AuthorId { get; }
        public PostTitle Title { get; }
        public PostContent Content { get; }
        public PostPublishedDate PublishedDate { get; }
        public PostUpdatedDate UpdatedDate { get; }
        public bool IsPinned { get; }

        private Post(
            PostId? id,
            Guid authorId,
            PostTitle title,
            PostContent content,
            PostPublishedDate publishedDate,
            PostUpdatedDate updatedDate,
            bool isPinned
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
                PublishedDate,
                updateDate,
                isPinned ?? IsPinned
                );
        }

        public static Post Create(
            PostId? id,
            Guid authorId,
            PostTitle title,
            PostContent content,
            PostPublishedDate publishedDate,
            PostUpdatedDate updatedDate,
            bool isPinned
            )
        {
            ValidateUpdateDate(publishedDate, updatedDate);

            return new(id, authorId, title, content, publishedDate, updatedDate, isPinned);
        }

        // Post manipulations


        // Validations
        private static void ValidateUpdateDate(PostPublishedDate published, PostUpdatedDate? updated)
        {
            if (updated is not null && updated.Value < published.Value)
                throw new InvalidPostUpdatedDateException("Updated date cannot be earlier than uploaded date.");
        }
    }
}
