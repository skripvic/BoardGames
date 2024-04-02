namespace BuisinessLogic.Commands.Users.Validation
{
    public interface IUserCommand
    {
        public string Name { get; init; }
        public string Email { get; init; }
    }
}
