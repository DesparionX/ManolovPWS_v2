namespace ManolovPWS_v2.Domain.Models.Post.Properties.PostContent
{
    public sealed class PostContent : IEquatable<PostContent>
    {
        public PostContext Context { get; }
        public PostPicture? Thumb { get; }
        public PostGallery? Gallery { get; }
        
        private PostContent(PostContext context, PostPicture? thumb = default, PostGallery? gallery = default)
        {
            Context = context;
            Thumb = thumb;
            Gallery = gallery ?? PostGallery.Empty();
        }

        public static PostContent Create(
            PostContext context,
            PostPicture? thumb = default,
            PostGallery? gallery = default
            )
            => new(context, thumb, gallery);

        // Content manipulations
        public PostContent UpdateContext(PostContext newContext)
            => new(newContext, Thumb, Gallery);

        public PostContent UpdateThumb(PostPicture thumb)
            => new(Context, thumb, Gallery);

        public PostContent UpdateGallery(PostGallery gallery)
            => new(Context, Thumb, gallery);

        // Equality
        public bool Equals(PostContent? other) =>
            other is not null
            && Context.Equals(other.Context)
            && (Thumb is not null && Thumb.Equals(other.Thumb))
            && (Gallery is not null && Gallery.Equals(other.Gallery));

        public override bool Equals(object? obj) => Equals(obj as PostContent);

        public override int GetHashCode() =>
            HashCode.Combine(
                Context.GetHashCode,
                Thumb?.GetHashCode(),
                Gallery?.GetHashCode()
                );
    }
}
