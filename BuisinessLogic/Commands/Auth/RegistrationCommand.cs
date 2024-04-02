using BuisinessLogic.Auth;
using BuisinessLogic.Auth.Base;
using BuisinessLogic.Exceptions;
using BuisinessLogic.Constants;
using DomainLayer.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using DataAccess;
using BuisinessLogic.Commands.Users.Validation;

namespace BuisinessLogic.Commands.Auth
{
    public class RegistrationCommand: IRequest<AuthResponse>, IUserCommand
    {
        public string Name { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, AuthResponse>
        {
            private readonly IAuthService _authService;
            private readonly UserManager<User> _userManager;
            private readonly IApplicationDbContext _context;
            private readonly UserCommandValidator _validator;

            public RegistrationCommandHandler(IAuthService authService, UserManager<User> userManager,
                IApplicationDbContext context, UserCommandValidator validator)
            {
                _authService = authService;
                _userManager = userManager;
                _context = context;
                _validator = validator;
            }

            public async Task<AuthResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
            {
                _validator.ValidateOrThrow(request);

                var newUser = new User
                (
                    request.Name,
                    request.Email
                );

                var result = await _userManager.CreateAsync(newUser, request.Password);

                if (result.Succeeded is false)
                {
                    throw new BadRequestException(
                    $"Ошибки при регистрации нового пользователя: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                foreach (var collection in CollectionConstants.defaultCollections) {
                    var newCollection = new Collection(collection, newUser);
                    _context.Collections.Add(newCollection);
                }
                await _context.SaveChangesAsync();

                return await _authService.SignInAsync(request.Email, request.Password, cancellationToken);
            }
        }
    }
}
