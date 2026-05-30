using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Post.Properties.PostContent
{
    public sealed class PostGallery : IEquatable<PostGallery>
    {
        private readonly List<PostPicture> _pictures;

        public IReadOnlyList<PostPicture> Pictures => _pictures;

        [JsonConstructor]
        private PostGallery(IReadOnlyList<PostPicture> pictures)
        {
            _pictures = [.. pictures];
        }

        public static PostGallery Empty() => new([]);

        public static PostGallery Create(IEnumerable<PostPicture> pictures) => new(pictures.ToList());

        // Gallery manipulations
        internal PostGallery AddPicture(PostPicture picture)
            => new(_pictures.Append(picture).ToList());

        internal PostGallery AddPictures(IEnumerable<PostPicture> pictures)
            => new(_pictures.Concat(pictures).ToList());

        internal PostGallery UpdatePicture(PostPicture oldPicture, PostPicture newPicture)
            => new(_pictures.Select(p => p.Equals(oldPicture) ? newPicture : p).ToList());

        internal PostGallery RemovePicture(PostPicture picture)
            => new(_pictures.Where(p => !p.Equals(picture)).ToList());

        internal PostGallery RemovePictures(IEnumerable<PostPicture> pictures)
            => new(_pictures.Except(pictures).ToList());

        // Equality
        public bool Equals(PostGallery? other) =>
            other is not null
            && _pictures.Count == other._pictures.Count
            && _pictures.OrderBy(s => s.Value).SequenceEqual(other._pictures.OrderBy(s => s.Value));

        public override bool Equals(object? obj) => Equals(obj as PostGallery);

        public override int GetHashCode()
        {
            var hash = new HashCode();
            foreach (var picture in _pictures)
            {
                hash.Add(picture.GetHashCode());
            }

            return hash.ToHashCode();
        }
    }
}
