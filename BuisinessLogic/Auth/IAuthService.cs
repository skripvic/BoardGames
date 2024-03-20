using BuisinessLogic.Auth.Base;
using DomainLayer.Entities;

namespace BuisinessLogic.Auth
{
    public interface IAuthService
    {
        String CreateAccessToken(User user);
        RefreshToken CreateRefreshToken(User user);
        User GetUserFromAccessToken(string accessToken);
        Task<AuthResponse> SignInAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<AuthResponse> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default);

    }
}
