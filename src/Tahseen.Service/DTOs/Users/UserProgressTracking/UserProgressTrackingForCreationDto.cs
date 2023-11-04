namespace Tahseen.Service.DTOs.Users.UserProgressTracking
{
    public class UserProgressTrackingForCreationDto
    {
        public long UserId { get; set; }
        public long BookId { get; set; }
        public long CurrentPage { get; set; }
        public long TotalPages { get; set; }
    }
}
