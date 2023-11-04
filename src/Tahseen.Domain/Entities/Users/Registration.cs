using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities;

public class Registration : Auditable
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}