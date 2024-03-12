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

        public Guid userId { get; init; }

        public class CreateCollectionCommandHandler : IRequestHandler<CreateCollectionCommand, CreateCollectionCommandResponse>
        {
            private readonly IApplicationDbContext _context;

            public CreateCollectionCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<CreateCollectionCommandResponse> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
            {
                //TODO: переделать когда будет авторизация (брать авторизованного пользователя)
                
                var user = await _context.Users.FindAsync(request.userId, cancellationToken);

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
