using ManolovPWS_v2.Domain.Models.Message;
using ManolovPWS_v2.Domain.Models.Message.Properties;

namespace ManolovPWS_v2.Domain.Contracts.Factories
{
    public interface IMessageFactory : IFactory<Message, MessageId>
    {
    }
}
