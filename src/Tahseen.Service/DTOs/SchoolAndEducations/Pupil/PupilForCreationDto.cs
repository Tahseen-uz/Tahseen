using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Entities.SchoolAndEducations;

namespace Tahseen.Service.DTOs.SchoolAndEducations;

public class PupilForCreationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Grade { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Image { get; set; }
    public long LibraryBranchId { get; set; }
}