namespace DomainLayer.Entities
{
    public class Game
    {
        public int Id { get; private set; }
        
        public string Alias { get; private set; } = string.Empty;
        
        public string TitleRussian { get; private set; } = string.Empty;
        
        public string TitleEnglish { get; private set; } = string.Empty;
        
        public string PhotoUrl { get; private set; } = string.Empty;
        
        public int PlayersMin { get; private set; }
        
        public int PlayersMax { get; private set; }
        
        public int AgeMin { get; private set; }
        
        public int PlayTimeMin { get; private set; }
        
        public int PlayTimeMax { get; private set; }
        
        public int Year { get; private set; }
        
        public Game() { }

    }
}
