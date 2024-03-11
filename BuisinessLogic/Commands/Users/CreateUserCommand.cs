using BuisinessLogic.Dto.Users;
using DataAccess;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace BuisinessLogic.Commands.Users
{
    public class CreateUserCommand : IRequest<CreateUserCommandResponse>
    {
        public string Name { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
        {
            private readonly IApplicationDbContext _context;

            public CreateUserCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var newUser = new User
                (
                    request.Name,
                    request.Email
                );

                _context.Users.Add(newUser);

                await _context.SaveChangesAsync(cancellationToken);

                return new CreateUserCommandResponse()
                {
                    Id = newUser.Id
                };
            }
        }
    }
}
