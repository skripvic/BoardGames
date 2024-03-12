using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuisinessLogic.Commands.Collections
{
    public class AddGameToCollectionCommand : IRequest
    {
        public int collectionId { get; init; }

        public int gameId { get; init; }

        public class AddGameToCollectionCommandHandler : IRequestHandler<AddGameToCollectionCommand>
        {
            private readonly IApplicationDbContext _context;

            public AddGameToCollectionCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(AddGameToCollectionCommand request, CancellationToken cancellationToken)
            {
                var game = await _context.Games.FindAsync(request.gameId, cancellationToken);

                if (game == null)
                {
                    throw new EntityNotFoundException("Игра не найдена");
                }

                var collection = await _context.Collections
                    .Include(x => x.Games)
                    .FirstOrDefaultAsync(x => x.Id == request.collectionId, cancellationToken);

                if (collection == null)
                {
                    throw new EntityNotFoundException("Коллекция не найдена");
                }

                if (collection.Games.Any(x => x.Id == game.Id))
                {
                    return;
                }

                collection.Games.Add(game);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
