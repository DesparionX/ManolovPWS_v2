using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Domain.Models.Message.Properties;
using ManolovPWS_v2.Domain.Models.Message.Properties.SenderInfo;

namespace ManolovPWS_v2.Infrastructure.Persistance.Entities
{
    public sealed class DbMessage : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Context { get; set; } = null!;
        public string SenderName { get; set; } = null!;
        public string SenderEmail { get; set;} = null!;
        public SenderMetadata SenderMetadata { get; set; } = null!;
        public DateTime SentDate { get; set; }
        public bool IsUnread { get; set; }
    }
}
