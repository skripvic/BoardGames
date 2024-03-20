using DataAccess.Settings;

namespace BuisinessLogic.Settings
{
    public sealed class AppSettings
    {
        public DatabaseSettings Database { get; set; }
        public AuthSettings Auth { get; set; }
    }
}
