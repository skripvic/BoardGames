using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;

namespace BuisinessLogic.Commands.Users
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; init; }

        public string UserName { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateUserCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.Id);

                if (user == null) 
                {
                    throw new EntityNotFoundException("Пользователь не найден");
                }

                user.UpdateUser(request.UserName, request.Email);

                await _context.SaveChangesAsync(cancellationToken);

            }
        }
    }
}
