namespace Tahseen.Service.DTOs.Users.Registration
{
    public class RegistrationForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string VerificationCode { get; set; }
    }
}
