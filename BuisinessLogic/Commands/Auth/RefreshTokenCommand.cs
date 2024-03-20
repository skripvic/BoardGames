using BuisinessLogic.Auth;
using BuisinessLogic.Auth.Base;
using DataAccess;
using MediatR;

namespace BuisinessLogic.Commands.Auth
{
    public class RefreshTokenCommand : IRequest<AuthResponse>
    {
        public string RefreshToken { get; init; } = string.Empty;

        public string AccessToken { get; init; } = string.Empty;

        public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
        {
            private readonly IAuthService _authService;

            public RefreshTokenCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
            {
                return await _authService.RefreshTokenAsync(request.AccessToken, request.RefreshToken, cancellationToken);
            }
        }
    }
}
