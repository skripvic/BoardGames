namespace BuisinessLogic.Settings
{
    public sealed class AuthSettings
    {
        public string? SecretKey { get; set; }
        public int AccessTokenLifetimeMinutes { get; set; }
        public int RefreshTokenLifetimeDays { get; set; }
    }
}
