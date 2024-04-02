using Azure.Core;
using BuisinessLogic.Commands.Users.Validation;
using BuisinessLogic.Dto.Users;
using DataAccess;
using DomainLayer.Entities;
using MediatR;

namespace BuisinessLogic.Commands.Users
{
    public class CreateUserCommand : IRequest<CreateUserCommandResponse>, IUserCommand
    {
        public string Name { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly UserCommandValidator _validator;

            public CreateUserCommandHandler(IApplicationDbContext context, UserCommandValidator validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                _validator.ValidateOrThrow(request);

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
