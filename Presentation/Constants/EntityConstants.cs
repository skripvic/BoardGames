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

        }

        public record Size(int Min, int Max)
        {
        
        }

    }
}
