using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Presentation.Constants
{
    public static class EntityConstants
    {
        public static class User
        {
            public static readonly Size Name = new(1, 128);
            
            public static readonly Size Email = new(1, 128);
            
        }

        public static class Collections
        {
            public static readonly Size Name = new(1, 128);

        }

        public static class Game
        {
            public static readonly Size Alias = new(1, 128);
            
            public static readonly Size TitleRussian = new(1, 128);
            
            public static readonly Size TitleEnglish = new(1, 128);
            
            public static readonly Size PhotoUrl = new(5, 512);

            public static readonly Size PlayersMin = new(1, 100);
            
            public static readonly Size PlayersMax = new(1, 100);
            
            public static readonly Size AgeMin = new(0, 21);

            public static readonly Size PlayTimeMin = new(1, 1440);
            
            public static readonly Size PlayTimeMax = new(1, 1440);

            public static readonly Size Year = new(1900, DateTime.Now.Year);

        }

        public record Size(int Min, int Max)
        {
        
        }

    }
}
