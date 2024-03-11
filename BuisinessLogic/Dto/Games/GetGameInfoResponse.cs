namespace BuisinessLogic.Dto.Games
{
    public class GetGameInfoResponse
    {
        public string Alias { get; init; } = string.Empty;

        public string TitleRussian { get; init; } = string.Empty;

        public string TitleEnglish { get; init; } = string.Empty;

        public string PhotoUrl { get; init; } = string.Empty;

        public int PlayersMin { get; init; }

        public int PlayersMax { get; init; }

        public int AgeMin { get; init; }

        public int PlayTimeMin { get; init; }

        public int PlayTimeMax { get; init; }

        public int Year { get; init; }

    }
}
