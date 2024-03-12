using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;

namespace BuisinessLogic.Commands.Users
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; init; }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteUserCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FindAsync(request.Id);

                if (user == null)
                {
                    throw new EntityNotFoundException("Пользователь не найден");
                }

                _context.Users.Remove(user);

                await _context.SaveChangesAsync(cancellationToken);

            }
        }
    }
}
