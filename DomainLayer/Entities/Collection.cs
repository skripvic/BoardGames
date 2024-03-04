namespace DomainLayer.Entities
{
    public class Collection
    {
        public int Id { get; private set; }
        
        public string Name { get; private set; }
        
        public User user { get; private set; }

        public IList<Game> Games { get; private set; } = new List<Game>();
        
        public Collection() { }

    }
}
