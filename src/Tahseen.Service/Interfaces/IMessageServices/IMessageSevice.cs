using Tahseen.Service.DTOs.Message;

namespace Tahseen.Service.Interfaces.IMessageServices
{
    public interface IMessageSevice
    {
        public Task SendEmail(MessageForCreationDto message);
    }
}
