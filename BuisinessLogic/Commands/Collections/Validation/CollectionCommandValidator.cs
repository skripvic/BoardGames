using BuisinessLogic.Exceptions;
using Presentation.Constants;

namespace BuisinessLogic.Commands.Collections.Validation
{
    public class CollectionCommandValidator
    {
        public void ValidateOrThrow<TCommand>(TCommand command)
        where TCommand : ICollectionCommand
        {
            var errors = new List<string>();

            if (command.Name == null!
                || command.Name.Length < EntityConstants.Collections.Name.Min
                || command.Name.Length > EntityConstants.Collections.Name.Max)
            {
                errors.Add("Некорректное имя");
            }

            if (errors.Count != 0)
            {
                throw new BadRequestException(string.Join("; ", errors));
            }
        }
    }
}
