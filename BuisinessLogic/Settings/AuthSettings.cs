namespace BuisinessLogic.Settings
{
    public sealed class AuthSettings
    {
        public string? SecretKey { get; set; }
        public int JwtLifetimeMinutes { get; set; }
    }
}
