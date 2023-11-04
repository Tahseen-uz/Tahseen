using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Library;

namespace Tahseen.Domain.Entities.SchoolAndEducations;

public class SchoolBook : Auditable
{
    public string SchoolBookTitle { get; set; }
    public string Grade { get; set; }
    public long LibraryBranchId { get; set; }
    public LibraryBranch LibraryBranch { get; set; }
}
