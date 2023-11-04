using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.DTOs.Feedbacks.UserMessages;
public class UserMessageForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public UserForResultDto User { get; set; }
    public string Message { get; set; }
}
