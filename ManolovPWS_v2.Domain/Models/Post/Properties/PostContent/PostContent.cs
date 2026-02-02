namespace ManolovPWS_v2.Domain.Models.Post.Properties.PostContent
{
    public sealed class PostContent : IEquatable<PostContent>
    {
        public PostContext Context { get; }
        public PostPicture? Thumb { get; }
        public PostGallery Gallery { get; }
        
        private PostContent(PostContext context, PostPicture? thumb = default, PostGallery? gallery = default)
        {
            Context = context;
            Thumb = thumb;
            Gallery = gallery ?? PostGallery.Empty();
        }

        private PostContent With(
            PostContext? context = default,
            PostPicture? thumb = default,
            PostGallery? gallery = default)
            => new(
                context ?? Context,
                thumb ?? Thumb,
                gallery ?? Gallery
                );

        public static PostContent Create(
            PostContext context,
            PostPicture? thumb = default,
            PostGallery? gallery = default
            )
            => new(context, thumb, gallery);

        // Content manipulations
        internal PostContent UpdateContext(PostContext newContext)
            => new(newContext, Thumb, Gallery);

        internal PostContent UpdateThumb(PostPicture thumb)
            => new(Context, thumb, Gallery);


        // Gallery manipulations
        internal PostContent ReplaceGallery(PostGallery gallery)
            => new(Context, Thumb, gallery);
        internal PostContent AddToGallery(PostPicture newPicture)
        {
            var withNewPicture = Gallery.AddPicture(newPicture);

            if (Gallery.Equals(withNewPicture)) return this;

            return With(gallery: withNewPicture);
        }
        internal PostContent AddToGallery(IEnumerable<PostPicture> newPictures)
        {
            var withNewPictures = Gallery.AddPictures(newPictures);

            if (Gallery.Equals(withNewPictures)) return this;

            return With(gallery: withNewPictures);
        }
        internal PostContent UpdateGalleryPicture(PostPicture oldPicture, PostPicture newPicture)
        {
            var withUpdatedPicture = Gallery.UpdatePicture(oldPicture, newPicture);

            if (Gallery.Equals(withUpdatedPicture)) return this;

            return With(gallery: withUpdatedPicture);
        }
        internal PostContent RemoveFromGallery(PostPicture pictureToRemove)
        {
            var withRemovedPicture = Gallery.RemovePicture(pictureToRemove);

            if (Gallery.Equals(withRemovedPicture)) return this;

            return With(gallery: withRemovedPicture);
        }
        internal PostContent RemoveFromGallery(IEnumerable<PostPicture> picturesToRemove)
        {
            var withRemovedPictures = Gallery.RemovePictures(picturesToRemove);

            if (Gallery.Equals(withRemovedPictures)) return this;

            return With(gallery: withRemovedPictures);
        }

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
