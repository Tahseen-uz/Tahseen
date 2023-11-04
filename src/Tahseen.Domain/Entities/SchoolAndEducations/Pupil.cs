using System;
using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Library;

namespace Tahseen.Domain.Entities.SchoolAndEducations;

public class Pupil:Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Grade { get; set; }
    public DateTime DateOfBirth { get; set; }
    public IEnumerable<PupilBookConnection> SubjectBooksBorrow { get; set; }
    public string Image { get; set; }
    public long LibraryBranchId { get; set; }
    public LibraryBranch LibraryBranch { get; set; }
}
