using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Users.UserSettings
{
    public class UserSettingsForCreationDto
    {
        public long UserId { get; set; }
        public NotificationStatus NotificationPreference { get; set; }
        public ThemePreference ThemePreference { get; set; }
        public LanguagePreference LanguagePreference { get; set; }
    }
}
