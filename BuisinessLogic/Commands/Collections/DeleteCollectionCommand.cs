using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;

namespace BuisinessLogic.Commands.Collections
{
    public class DeleteCollectionCommand : IRequest
    {
        public int Id { get; init; }

        public class DeleteCollectionCommandHandler : IRequestHandler<DeleteCollectionCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteCollectionCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
            {
                //TODO: переделать когда будет авторизация

                var collection = await _context.Collections.FindAsync(request.Id);

                if (collection == null)
                {
                    throw new EntityNotFoundException("Коллекция не найдена");
                }

                _context.Collections.Remove(collection);

                await _context.SaveChangesAsync();
            }
        }
    }
}
