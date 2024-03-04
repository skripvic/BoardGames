namespace DomainLayer.Entities
{
    public class Game
    {
        public int Id { get; private set; }
        
        public string Alias { get; private set; }
        
        public string TitleRussian { get; private set; }
        
        public string TitleEnglish { get; private set; }
        
        public string photoUrl { get; private set; }
        
        public int playersMin { get; private set; }
        
        public int playersMax { get; private set; }
        
        public int ageMin { get; private set; }
        
        public int playTimeMin { get; private set; }
        
        public int playTimeMax { get; private set; }
        
        public int year { get; private set; }
        
        public Game() { }

    }
}
