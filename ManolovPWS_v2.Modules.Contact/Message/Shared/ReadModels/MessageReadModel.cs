using ManolovPWS_v2.Modules.Contact.Message.Shared.Properties;

namespace ManolovPWS_v2.Modules.Contact.Message.Shared.ReadModels
{
    public sealed record MessageReadModel(
        string Id,
        string SenderName,
        string SenderEmail,
        SenderMetadataDto SenderMetadata,
        string Title,
        string Condext,
        DateTime SentDate,
        bool IsUnread
    );
}
