using BuisinessLogic.Auth;
using BuisinessLogic.Auth.Base;
using BuisinessLogic.Exceptions;
using DomainLayer.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BuisinessLogic.Commands.Auth
{
    public class RegistrationCommand: IRequest<AuthResponse>
    {
        public string UserName { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, AuthResponse>
        {
            private readonly IAuthService _authService;
            private readonly UserManager<User> _userManager;

            public RegistrationCommandHandler(IAuthService authService, UserManager<User> userManager)
            {
                _authService = authService;
                _userManager = userManager;
            }

            public async Task<AuthResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
            {
                var newUser = new User
                (
                    request.UserName,
                    request.Email
                );

                var result = await _userManager.CreateAsync(newUser, request.Password);

                if (result.Succeeded is false)
                {
                    throw new BadRequestException(
                    $"Ошибки при регистрации нового пользователя: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                return await _authService.SignInAsync(request.Email, request.Password, cancellationToken);
            }
        }
    }
}
