using Microsoft.AspNetCore.Identity;

namespace DomainLayer.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; } = string.Empty;

        public override string Email { get; set; } = string.Empty;

        public User() { }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
            UserName = email;
        }
        
        public void UpdateUser(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
