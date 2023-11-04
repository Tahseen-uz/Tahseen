using Microsoft.AspNetCore.Http;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Users.User
{
    public class UserForUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public MembershipStatus MembershipStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IFormFile UserImage { get; set; }
        public Roles Role { get; set; }
    }
}
