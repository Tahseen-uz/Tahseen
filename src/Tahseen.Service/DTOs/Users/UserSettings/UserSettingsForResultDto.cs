using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Users.UserSettings
{
    public class UserSettingsForResultDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string NotificationPreference { get; set; }
        public string ThemePreference { get; set; }
        public string LanguagePreference { get; set; }
    }
}
