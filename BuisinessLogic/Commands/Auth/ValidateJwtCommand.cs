using BuisinessLogic.Auth;
using BuisinessLogic.Auth.Base;
using MediatR;

namespace BuisinessLogic.Commands.Auth
{
    public class ValidateJwtCommand : IRequest<ValidateJwtResponse>
    {
        public string jwt { get; init; } = string.Empty;

        public class ValidateJwtCommandHandler : IRequestHandler<ValidateJwtCommand, ValidateJwtResponse>
        {
            private readonly IAuthService _authService;

            public ValidateJwtCommandHandler(IAuthService authService)
            {
                _authService = authService;
            }

            public Task<ValidateJwtResponse> Handle(ValidateJwtCommand request, CancellationToken cancellationToken)
            {
                return _authService.ValidateJwt(request.jwt);
            }
        }
    }
}
