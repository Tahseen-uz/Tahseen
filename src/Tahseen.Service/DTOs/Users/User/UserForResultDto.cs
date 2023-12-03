using Tahseen.Domain.Entities;
using Tahseen.Domain.Enums;
using Tahseen.Service.DTOs.Users.BorrowedBook;

namespace Tahseen.Service.DTOs.Users.User
{
    public class UserForResultDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public decimal FineAmount { get; set; }
        public string UserImage { get; set; }
        public string Roles { get; set; }

        public IEnumerable<BorrowedBookForResultDto> BorrowedBooks { get; set; }

    }
}
