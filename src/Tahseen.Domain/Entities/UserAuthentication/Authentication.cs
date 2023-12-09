using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities;

public class Authentication:Auditable
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}