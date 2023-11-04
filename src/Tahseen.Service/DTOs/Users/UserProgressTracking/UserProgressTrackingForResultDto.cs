namespace Tahseen.Service.DTOs.Users.UserProgressTracking
{
    public class UserProgressTrackingForResultDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public long CurrentPage { get; set; }
        public long TotalPages { get; set; }
    }
}
