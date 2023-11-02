﻿using Tahseen.Domain.Entities.Librarians;
using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Libraries.LibraryBranch;

public class LibraryBranchForCreationDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string OpeningHours { get; set; }
    public LibraryType LibraryType { get; set; }

}
