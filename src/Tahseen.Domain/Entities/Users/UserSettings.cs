using Tahseen.Domain.Commons;
using Tahseen.Domain.Enums;

namespace Tahseen.Domain.Entities;

public class UserSettings:Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public NotificationStatus NotificationPreference { get; set; }
    public ThemePreference ThemePreference { get; set; }
    public LanguagePreference LanguagePreference { get; set; }
}