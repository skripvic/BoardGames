using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;

namespace BuisinessLogic.Commands.Collections
{
    public class UpdateCollectionCommand : IRequest
    {
        public int Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public class UpdateCollectionCommandHandler : IRequestHandler<UpdateCollectionCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateCollectionCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(UpdateCollectionCommand request, CancellationToken cancellationToken)
            {
                //TODO: переделать когда будет авторизация

                var collection = await _context.Collections.FindAsync(request.Id);

                if (collection == null) 
                {
                    throw new EntityNotFoundException("Коллекция не найдена");
                }

                collection.UpdateCollection(request.Name);

                await _context.SaveChangesAsync();
            }
        }
    }
}
