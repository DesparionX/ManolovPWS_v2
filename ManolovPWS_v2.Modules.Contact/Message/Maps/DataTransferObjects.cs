using ManolovPWS_v2.Domain.Models.Message;
using ManolovPWS_v2.Domain.Models.Message.Properties.SenderInfo;
using ManolovPWS_v2.Modules.Contact.Message.Shared.Properties;

namespace ManolovPWS_v2.Modules.Contact.Message.Maps
{
    public static class DataTransferObjects
    {
        public static SenderMetadata ToDomainSenderMetadata (this SenderMetadataDto senderMetadataDto)
            => SenderMetadata.Create(
                ipAddress: senderMetadataDto.IpAddress,
                userAgent: senderMetadataDto.UserAgent
            );

    }
}
