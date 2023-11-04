using System.ComponentModel.DataAnnotations;
using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities.Rewards;

public class Badge : Auditable
{
    [Required]
    [MinLength(1), MaxLength(64)]
    public string Name { get; set; }

    [Required]
    public string ImageUrl { get; set; }
}
