using BuisinessLogic.Auth.Base;
using DomainLayer.Entities;

namespace BuisinessLogic.Auth
{
    public interface IAuthService
    {
        String CreateJwt(User user);
        User GetUserFromJwt(string accessToken);
        Task<AuthResponse> SignInAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<ValidateJwtResponse> ValidateJwt(string Jwt);

    }
}
