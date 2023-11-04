using Microsoft.AspNetCore.Http;
using System.Data;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Enums;
using Tahseen.Service.DTOs.Users.BorrowedBook;

namespace Tahseen.Service.DTOs.Users.User
{
    public class UserForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public MembershipStatus MembershipStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Roles Role { get; set; }
        public decimal FineAmount { get; set; }
        public IFormFile UserImage { get; set; }
    }
}
