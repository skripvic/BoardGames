namespace DomainLayer.Entities
{
    public class User
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public string Password { get; private set; } = string.Empty;
        
        public User() { }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
