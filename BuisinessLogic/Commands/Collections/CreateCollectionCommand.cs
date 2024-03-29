using BuisinessLogic.Auth.CurrentUser;
using BuisinessLogic.Dto.Collections;
using BuisinessLogic.Exceptions;
using DataAccess;
using DomainLayer.Entities;
using MediatR;

namespace BuisinessLogic.Commands.Collections
{
    public class CreateCollectionCommand : IRequest<CreateCollectionCommandResponse>
    {
        public string Name { get; init; } = string.Empty;

        public class CreateCollectionCommandHandler : IRequestHandler<CreateCollectionCommand, CreateCollectionCommandResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUser _currentUser;

            public CreateCollectionCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }


            public async Task<CreateCollectionCommandResponse> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
            {                
                var user = await _context.Users.FindAsync(_currentUser.Id, cancellationToken);

                if (user == null) 
                {
                    throw new EntityNotFoundException("Пользователь не найден");
                }

                var newCollection = new Collection(request.Name, user);

                _context.Collections.Add(newCollection);

                await _context.SaveChangesAsync();

                return new CreateCollectionCommandResponse
                    { 
                        Id = newCollection.Id 
                    };
            }
        }
    }
}
