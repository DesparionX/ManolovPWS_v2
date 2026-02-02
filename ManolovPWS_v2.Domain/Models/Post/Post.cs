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
        public Post PinPost() => IsPinned ? this : With(isPinned: true);
        public Post UnpinPost() => !IsPinned ? this : With(isPinned: false);
        public Post UpdateTitle(PostTitle newTitle)
        {
            if (Title.Equals(newTitle)) return this;

            return With(title: newTitle);
        }

        // Content manipulations
        public Post ReplaceContent(PostContent newContent)
        {
            if (Content.Equals(newContent)) return this;

            return With(content: newContent);
        }
        public Post UpdateContext(PostContext newContext)
        {
            var withUpdatedContext = Content.UpdateContext(newContext);

            if (Content.Equals(withUpdatedContext)) return this;

            return With(content: withUpdatedContext);
        }
        public Post UpdateThumb(PostPicture newThumb)
        {
            var withUpdatedThumb = Content.UpdateThumb(newThumb);

            if (Content.Equals(withUpdatedThumb)) return this;

            return With(content: withUpdatedThumb);
        }

        // Content - Gallery manipulations
        public Post ReplaceGallery(PostGallery newGallery)
        {
            var withNewGallery = Content.ReplaceGallery(newGallery);

            if (Content.Equals(withNewGallery)) return this;
            
            return With(content: withNewGallery);
        }
        public Post AddToGallery(PostPicture newPicture)
        {
            var withUpdatedGallery = Content.AddToGallery(newPicture);

            if (Content.Equals(withUpdatedGallery)) return this;

            return With(content: withUpdatedGallery);
        }
        public Post AddToGallery(IEnumerable<PostPicture> newPictures)
        {
            var withUpdatedContent = Content.AddToGallery(newPictures);

            if (Content.Equals(withUpdatedContent)) return this;

            return With(content: withUpdatedContent);
        }
        public Post UpdateGalleryPicture(PostPicture oldPicture,  PostPicture newPicture)
        {
            var withUpdatedContent = Content.UpdateGalleryPicture(oldPicture, newPicture);

            if (Content.Equals(withUpdatedContent)) return this;

            return With(content: withUpdatedContent);
        }
        public Post RemoveFromGallery(PostPicture pictureToRemove)
        {
            var withUpdatedContent = Content.RemoveFromGallery(pictureToRemove);

            if (Content.Equals(withUpdatedContent)) return this;

            return With(content: withUpdatedContent);
        }

        // Validations
        private static void ValidateUpdateDate(PostPublishedDate published, PostUpdatedDate? updated)
        {
            if (updated is not null && updated.Value < published.Value)
                throw new InvalidPostUpdatedDateException("Updated date cannot be earlier than uploaded date.");
        }
    }
}
