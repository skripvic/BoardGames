using BuisinessLogic.Commands.Users.Validation;
using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;

namespace BuisinessLogic.Commands.Users
{
    public class UpdateUserCommand : IRequest, IUserCommand
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly UserCommandValidator _validator;

            public UpdateUserCommandHandler(IApplicationDbContext context, UserCommandValidator validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                _validator.ValidateOrThrow(request);

                var user = await _context.Users.FindAsync(request.Id);

                if (user == null) 
                {
                    throw new EntityNotFoundException("Пользователь не найден");
                }

                user.UpdateUser(request.Name, request.Email);

                await _context.SaveChangesAsync(cancellationToken);

            }
        }
    }
}
