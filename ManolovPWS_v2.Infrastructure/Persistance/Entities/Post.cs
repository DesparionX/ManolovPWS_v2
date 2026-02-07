using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Domain.Models.Post.Properties.PostContent;

namespace ManolovPWS_v2.Infrastructure.Persistance.Entities
{
    public sealed class Post : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; } = null!;
        public PostContent Content { get; set; } = null!;
        public DateOnly PublishedDate { get; set; }
        public DateOnly? UpdatedDate { get; set; }
        public bool IsPinned { get; set; }
    }
}
