using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Entities.SchoolAndEducations;
using Tahseen.Service.DTOs.Libraries.LibraryBranch;

namespace Tahseen.Service.DTOs.SchoolAndEducations;

public class PupilForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Grade { get; set; }
    public DateTime DateOfBirth { get; set; }
    public IEnumerable<PupilBookConnection> SubjectBooksBorrow { get; set; }
    public string Image { get; set; }
    public long LibraryBranchId { get; set; }
    public LibraryBranchForResultDto LibraryBranch { get; set; }
}