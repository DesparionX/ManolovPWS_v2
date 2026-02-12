using ManolovPWS_v2.Domain.Models.Post;
using ManolovPWS_v2.Domain.Models.Post.Properties;
using ManolovPWS_v2.Domain.Models.Post.Properties.PostContent;
using ManolovPWS_v2.Infrastructure.Exceptions;
using ManolovPWS_v2.Infrastructure.Persistance.Entities;

namespace ManolovPWS_v2.Infrastructure.Contracts.Maps
{
    public static class PostExtensions
    {
        public static Post ToDomain(this DbPost post)
        {
            if (post is null)
                throw new InfrastructureException("DbPost cannot be null.", "NullDbPostException");

            return Post.Create(
                PostId.From(post.Id.ToString()),
                post.AuthorId,
                PostTitle.Create(post.Title),
                PostContent.Create(post.Content.Context, post.Content.Thumb, post.Content.Gallery),
                post.IsPinned,
                PostPublishedDate.Create(post.PublishedDate),
                PostUpdatedDate.CreateOrNull(post.UpdatedDate)
                );
        }

        public static DbPost ToDbEntity(this Post post)
        {
            if (post is null)
                throw new InfrastructureException("Post cannot be null.", "NullPostException");

            return new DbPost
            {
                Id = Guid.Parse(post.Id.ToString()),
                AuthorId = post.AuthorId,
                Title = post.Title.Value,
                Content = post.Content,
                PublishedDate = post.PublishedDate.Value,
                UpdatedDate = post.UpdatedDate?.Value,
                IsPinned = post.IsPinned
            };
        }

        public static IReadOnlyList<Post> ToDomainList(this IReadOnlyList<DbPost> posts)
            => posts.Select(p => p.ToDomain()).ToList();

        public static IReadOnlyList<DbPost> ToEntityList(this IReadOnlyList<Post> posts)
            => posts.Select(p => p.ToDbEntity()).ToList();
    }
}
