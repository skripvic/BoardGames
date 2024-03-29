namespace BuisinessLogic.Auth.CurrentUser
{
    public interface ICurrentUser
    {
        Guid Id { get; }
        string Name { get; }
    }
}
