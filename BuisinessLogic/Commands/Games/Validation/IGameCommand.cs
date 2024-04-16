namespace BuisinessLogic.Commands.Games.Validation
{
    public interface IGameCommand
    {
        public string Alias { get; init; }

        public string TitleEnglish { get; init; }

        public string TitleRussian { get; init; }

        public int PlayersMin { get; init; }

        public int PlayersMax { get; init; }

        public int AgeMin { get; init; }

        public int PlayTimeMin { get; init; }

        public int PlayTimeMax { get; init; }

        public int Year { get; init; }
    }
}
