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

namespace BuisinessLogic.Auth.AuthService
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

        public string CreateJwt(User user)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new(AppClaimTypes.UserId, user.Id.ToString()),
                new(AppClaimTypes.UserEmail, user.Email),
                new(AppClaimTypes.IssuedAt, now.ToString()),
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.SecretKey!));

            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(_authSettings.JwtLifetimeMinutes)),
                notBefore: now,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public User GetUserFromJwt(string jwt)
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

            var principal = tokenHandler.ValidateToken(jwt, tokenValidationParameters, out var securityToken);
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
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null || await _userManager.CheckPasswordAsync(user, password) is false)
            {
                throw new BadRequestException("Неверный логин или пароль");
            }

            var jwt = CreateJwt(user);
            await _userManager.UpdateAsync(user);

            return new AuthResponse(jwt);
        }

        public Task<ValidateJwtResponse> ValidateJwt(string jwt)
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

            try 
            {
                var principal = tokenHandler.ValidateToken(jwt, tokenValidationParameters, out var securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken
                    || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return Task.FromResult(new ValidateJwtResponse(false));
                }

                return Task.FromResult(new ValidateJwtResponse(true)); 
            }
            catch (Exception ex)
            {
                return Task.FromException<ValidateJwtResponse>(ex);
            }
        }
    }
}
