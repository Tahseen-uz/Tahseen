using Microsoft.AspNetCore.Http;

namespace Tahseen.Service.DTOs.Users.User
{
    public class UserImageUpdateDto
    {
        public IFormFile UserImage { get; set; }
    }
}
