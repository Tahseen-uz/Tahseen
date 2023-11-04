using Tahseen.Domain.Entities;
using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Users.User
{
    public class UserForResultDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public MembershipStatus MembershipStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public IEnumerable<BorrowedBookForResultDto> BorrowedBooks { get; set; }
        public decimal FineAmount { get; set; }
        public Roles Role { get; set; }
        public string UserImage { get; set; }
    }
}
