namespace Tahseen.Service.DTOs.Books.BookRentalPermission
{
    public class BookRentalPermissionForCreationDto
    {
        public long UserId { get; set; }
        public long BookId { get; set; }
        public long LibraryBranchId { get; set; }
    }
}
