﻿namespace BuisinessLogic.Dto.Collections
{
    public class GetGamesInCollectionListDto
    {
        public int Id { get; init; }

        public string Alias { get; init; } = string.Empty;

        public string TitleRussian { get; init; } = string.Empty;

        public string TitleEnglish { get; init; } = string.Empty;

        public string PhotoUrl { get; init; } = string.Empty;
    }
}
