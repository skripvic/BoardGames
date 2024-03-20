using BuisinessLogic.Auth.Base;
using BuisinessLogic.Exceptions;
using BuisinessLogic.Settings;
using DataAccess;
using DomainLayer.Auth;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuisinessLogic.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthSettings _authSettings;
        private readonly IApplicationDbContext _dbContext;


        public AuthService(UserManager<User> userManager, AuthSettings authSettings, IApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _authSettings = authSettings;
            _dbContext = dbContext;
        }

        public String CreateAccessToken(User user)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new(AppClaimTypes.UserId, user.Id.ToString()),
                new(AppClaimTypes.UserName, user.UserName),
                new(AppClaimTypes.IssuedAt, now.ToString()),
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.SecretKey!));

            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(_authSettings.AccessTokenLifetimeMinutes)),
                notBefore: now,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public RefreshToken CreateRefreshToken(User user)
        {
            var now = DateTime.UtcNow;
            var refreshToken = new RefreshToken(user, now, now.AddDays(_authSettings.RefreshTokenLifetimeDays));

            user.AddRefreshToken(refreshToken);
            return refreshToken;
        }

        public User GetUserFromAccessToken(string accessToken)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.SecretKey!));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg
                    .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == AppClaimTypes.UserId);

            if (userIdClaim is null)
            {
                throw new UnauthorizedException("UserId claim is not set");
            }

            var user = _userManager.Users
                .FirstOrDefault(u => u.Id == Guid.Parse(userIdClaim.Value));

            if (user is null)
            {
                throw new UnauthorizedException("User is not found");
            }

            return user;
        }

        public async Task<AuthResponse> SignInAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user is null || await _userManager.CheckPasswordAsync(user, password) is false)
            {
                throw new BadRequestException("Неверный логин или пароль");
            }

            await _dbContext.RefreshTokens
                .Where(t => t.ValidUntil <= DateTime.UtcNow)
                .ExecuteDeleteAsync(cancellationToken);

            var accessToken = CreateAccessToken(user);
            var refreshToken = CreateRefreshToken(user);
            await _userManager.UpdateAsync(user);

            return new AuthResponse(accessToken, refreshToken.Id.ToString());
        }

        public async Task<AuthResponse> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken)
        {
            var user = GetUserFromAccessToken(accessToken);

            var existingRefreshToken = await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(t => t.Id == Guid.Parse(refreshToken), cancellationToken);

            if (existingRefreshToken is null)
            {
                throw new BadRequestException("Invalid access or refresh token.");
            }

            if (existingRefreshToken.ValidUntil <= DateTime.UtcNow)
            {
                _dbContext.RefreshTokens.Remove(existingRefreshToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                throw new BadRequestException("Invalid access or refresh token.");
            }

            var newAccessToken = CreateAccessToken(user);
            var newRefreshToken = CreateRefreshToken(user);

            _dbContext.RefreshTokens.Remove(existingRefreshToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new AuthResponse(newAccessToken, newRefreshToken.Id.ToString());
        }
    }
}
