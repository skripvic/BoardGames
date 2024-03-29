using BuisinessLogic.Exceptions;
using DomainLayer.Auth;
using System.Data.Entity.Core.Metadata.Edm;
using System.Security.Claims;

namespace BuisinessLogic.Auth.CurrentUser
{
    public class CurrentUser : ICurrentUser
    {
        public Guid Id { get; }
        public string Name { get; }

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            var context = httpContextAccessor.HttpContext
                ?? throw new BadRequestException($"{nameof(IHttpContextAccessor)} не задан");

            if (context.User.Identity is not ClaimsIdentity identity)
            {
                throw new BadRequestException($"{nameof(ClaimsIdentity)} не задана");
            }

            Id = identity.FindFirst(AppClaimTypes.UserId) is { } id
                ? Guid.Parse(id.Value)
                : throw new BadRequestException($"Claim {AppClaimTypes.UserId} не найден");
            Name = identity.FindFirst(AppClaimTypes.UserEmail) is { } name
                ? name.Value
                : throw new BadRequestException($"Claim {AppClaimTypes.UserEmail} не найден");
        }
    }
}
