using Microsoft.AspNetCore.Identity;

namespace DomainLayer.Entities
{
    public class User : IdentityUser<Guid>
    {
        public override string UserName { get; set; } = string.Empty;

        public override string Email { get; set; } = string.Empty;

        public IList<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

        public void AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshTokens.Add(refreshToken);
        }

        public User() { }

        public User(string name, string email)
        {
            UserName = name;
            Email = email;
        }
        
        public void UpdateUser(string name, string email)
        {
            UserName = name;
            Email = email;
        }
    }
}
