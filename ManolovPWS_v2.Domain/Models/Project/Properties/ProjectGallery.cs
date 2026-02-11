using System.Text.Json.Serialization;

namespace ManolovPWS_v2.Domain.Models.Project.Properties
{
    public sealed class ProjectGallery : IEquatable<ProjectGallery>
    {
        private readonly List<ProjectPicture> _pictures;

        public IReadOnlyCollection<ProjectPicture> Pictures => _pictures;

        [JsonConstructor]
        private ProjectGallery(IEnumerable<ProjectPicture> pictures)
        {
            _pictures = [.. pictures];
        }

        public static ProjectGallery Empty() => new([]);

        public static ProjectGallery Create(IEnumerable<ProjectPicture>? pictures)
        {
            if (pictures is null || !pictures.Any()) return Empty();

            return new(pictures);
        }

        public static ProjectGallery? From(ProjectGallery? gallery)
            => gallery is not null ? new(gallery.Pictures) : null;

        // Gallery manipulations
        internal ProjectGallery AddPicture(ProjectPicture picture)
            => new(_pictures.Append(picture));

        internal ProjectGallery AddPictures(IEnumerable<ProjectPicture> pictures)
            => new(_pictures.Concat(pictures));

        internal ProjectGallery RemovePicture(ProjectPicture picture)
            => new(_pictures.Where(p => !p.Equals(picture)));

        internal ProjectGallery RemovePictures(IEnumerable<ProjectPicture> pictures)
            => new(_pictures.Except(pictures));

        internal ProjectGallery UpdatePicture(ProjectPicture oldPicture, ProjectPicture newPicture)
            => new(_pictures.Select(p => p.Equals(oldPicture) ? newPicture : p));

        // Equality
        public bool Equals(ProjectGallery? other) =>
            other is not null
            && _pictures.Count == other._pictures.Count
            && _pictures.OrderBy(s => s.Value).SequenceEqual(other._pictures.OrderBy(s => s.Value))
            && !_pictures.Except(other._pictures).Any();

        public override bool Equals(object? obj) => Equals(obj as ProjectGallery);

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
