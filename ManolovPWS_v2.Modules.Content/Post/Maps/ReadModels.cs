using ManolovPWS_v2.Modules.Content.Post.Shared.ReadModels;

namespace ManolovPWS_v2.Modules.Content.Post.Maps
{
    public static class ReadModels
    {
        public static PostReadModel ToReadModel(this Domain.Models.Post.Post post) =>
            new(
                Id: post.Id.Value.ToString(),
                AuthorId: post.AuthorId.ToString(),
                Title: post.Title.Value,
                Context: post.Content.Context.Value,
                Thumb: post.Content.Thumb?.Value.ToString(),
                Gallery: post.Content.Gallery.Pictures.Select(p => p.Value.ToString()),
                PublishedDate: post.PublishedDate.Value,
                UpdatedDate: post.UpdatedDate?.Value,
                IsPinned: post.IsPinned
            );
    }
}
