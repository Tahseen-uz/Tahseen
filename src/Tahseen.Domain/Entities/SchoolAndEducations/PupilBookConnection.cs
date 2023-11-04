using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Library;

namespace Tahseen.Domain.Entities.SchoolAndEducations;

public class PupilBookConnection : Auditable
{
    public long PupilId { get; set; }
    public Pupil Pupil { get; set; }
    public long SchoolBookId { get; set; }
    public SchoolBook SchoolBook { get; set; }
    public long LibraryBranchId { get; set; }
    public LibraryBranch LibraryBranch { get; set; }
}