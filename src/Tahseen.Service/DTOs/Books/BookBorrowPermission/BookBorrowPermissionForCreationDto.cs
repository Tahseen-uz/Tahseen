namespace Tahseen.Service.DTOs.Books.BookBorrowPermission
{
    public class BookBorrowPermissionForCreationDto
    {
        public long UserId { get; set; }
        public long BookId { get; set; }
        public long LibraryBranchId { get; set; }
    }
}
