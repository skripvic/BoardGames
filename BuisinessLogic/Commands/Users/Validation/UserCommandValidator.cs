using BuisinessLogic.Exceptions;
using Presentation.Constants;

namespace BuisinessLogic.Commands.Users.Validation
{
    public class UserCommandValidator
    {
        public void ValidateOrThrow<TCommand>(TCommand command)
        where TCommand : IUserCommand
        {
            var errors = new List<string>();

            if (command.Name == null!
                || command.Name.Length < EntityConstants.User.Name.Min
                || command.Name.Length > EntityConstants.User.Name.Max)
            {
                errors.Add("Некорректное имя");
            }

            if (command.Email == null!
                || command.Email.Length < EntityConstants.User.Email.Min
                || command.Email.Length > EntityConstants.User.Email.Max)
            {
                errors.Add("Некорректный почтовый адрес");
            }

            if (errors.Count != 0)
            {
                throw new BadRequestException(string.Join("; ", errors));
            }
        }
    }
}
