using BuisinessLogic.Exceptions;
using Presentation.Constants;

namespace BuisinessLogic.Commands.Games.Validation
{
    public class GameCommandValidator
    {
        public void ValidateOrThrow<TCommand>(TCommand command)
        where TCommand : IGameCommand
        {
            var errors = new List<string>();

            if (command.Alias == null!
                || command.Alias.Length < EntityConstants.Game.Alias.Min
                || command.Alias.Length > EntityConstants.Game.Alias.Max)
            {
                errors.Add("Некорректный алиас");
            }

            if (command.TitleEnglish == null!
                || command.TitleEnglish.Length < EntityConstants.Game.TitleEnglish.Min
                || command.TitleEnglish.Length > EntityConstants.Game.TitleEnglish.Max)
            {
                errors.Add("Некорректное название на английском");
            }
            
            if (command.TitleRussian == null!
                || command.TitleRussian.Length < EntityConstants.Game.TitleRussian.Min
                || command.TitleRussian.Length > EntityConstants.Game.TitleRussian.Max)
            {
                errors.Add("Некорректное название на русском");
            }

            if (command.PlayersMin < EntityConstants.Game.PlayersMin.Min
                || command.PlayersMin > EntityConstants.Game.PlayersMin.Max)
            {
                errors.Add("Некорректное минимальное количество игроков");
            }
            
            if (command.PlayersMax < EntityConstants.Game.PlayersMax.Min
                || command.PlayersMax > EntityConstants.Game.PlayersMax.Max)
            {
                errors.Add("Некорректное максимальное количество игроков");
            }

            if (command.PlayersMax < command.PlayersMin)
            {
                errors.Add("Максимальное количество игроков не может быть меньше минимального");
            }
            
            if (command.AgeMin < EntityConstants.Game.AgeMin.Min
                || command.AgeMin > EntityConstants.Game.AgeMin.Max)
            {
                errors.Add("Некорректное возрастное ограничение");
            }
            
            if (command.PlayTimeMin < EntityConstants.Game.PlayTimeMin.Min
                || command.PlayTimeMin > EntityConstants.Game.PlayTimeMin.Max)
            {
                errors.Add("Некорректное минимальное время игры");
            }
            
            if (command.PlayTimeMax < EntityConstants.Game.PlayTimeMax.Min
                || command.PlayTimeMax > EntityConstants.Game.PlayTimeMax.Max)
            {
                errors.Add("Некорректное максимальное время игры");
            }

            if (command.PlayersMax < command.PlayersMin)
            {
                errors.Add("Максимальное время игры не может быть меньше минимального");
            }

            if (command.Year < EntityConstants.Game.Year.Min
                || command.Year > EntityConstants.Game.Year.Max)
            {
                errors.Add("Некорректный год выпуска");
            }

            if (errors.Count != 0)
            {
                throw new BadRequestException(string.Join("; ", errors));
            }
        }
    }
}
