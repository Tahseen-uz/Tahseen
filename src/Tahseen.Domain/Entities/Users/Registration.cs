using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities;

public class Registration : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string EmailAddress { get; set; }
}