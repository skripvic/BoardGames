namespace DomainLayer.Entities
{
    public class Collection
    {
        public int Id { get; private set; }
        
        public string Name { get; private set; } = string.Empty;
        
        public User User { get; private set; } = new User();

        public IList<Game> Games { get; private set; } = new List<Game>();
        
        public Collection() { }

    }
}
