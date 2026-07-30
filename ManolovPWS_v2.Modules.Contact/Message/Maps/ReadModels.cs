using ManolovPWS_v2.Domain.Models.Message.Properties.SenderInfo;
using ManolovPWS_v2.Modules.Contact.Message.Shared.Properties;
using ManolovPWS_v2.Modules.Contact.Message.Shared.ReadModels;

namespace ManolovPWS_v2.Modules.Contact.Message.Maps
{
    public static class ReadModels
    {
        public static MessageReadModel ToMessageReadModel(this Domain.Models.Message.Message message)
            => new(
                Id: message.Id.Value.ToString(),
                SenderName: message.Sender.Username.Value,
                SenderEmail: message.Sender.Email.Value,
                SenderMetadata: message.Sender.Metadata.ToSenderMetadataDto(),
                Title: message.Data.Title.Value,
                Condext: message.Data.Context.Value,
                SentDate: message.SentDate.Value,
                IsUnread: message.IsUnread
            );

        public static SenderMetadataDto ToSenderMetadataDto(this SenderMetadata senderMetadata)
            => new(
                IpAddress: senderMetadata.IpAddress,
                UserAgent: senderMetadata.UserAgent
            );
    }
}
