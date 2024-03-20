namespace DomainLayer.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }
        
        public User Owner { get; private set; }
        
        public DateTime IssuedAt { get; private set; }
        
        public DateTime ValidUntil { get; private set; }

        protected RefreshToken() { }

        public RefreshToken(User owner, DateTime issuedAt, DateTime validUntil)
        {
            Owner = owner;
            IssuedAt = issuedAt;
            ValidUntil = validUntil;
        }

    }
}
