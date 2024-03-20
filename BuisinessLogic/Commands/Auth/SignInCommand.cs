using BuisinessLogic.Auth;
using BuisinessLogic.Auth.Base;
using DataAccess;
using MediatR;

namespace BuisinessLogic.Commands.Auth
{
    public class SignInCommand : IRequest<AuthResponse>
    {
        public string Email { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public class SignInCommandHandler : IRequestHandler<SignInCommand, AuthResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly IAuthService _authService;

            public SignInCommandHandler(IApplicationDbContext context, IAuthService authService)
            {
                _context = context;
                _authService = authService;
            }

            public async Task<AuthResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
            {
                return await _authService.SignInAsync(request.Email, request.Password, cancellationToken);

            }
        }
    }
}
