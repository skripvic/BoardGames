using BuisinessLogic.Auth.CurrentUser;
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
            private readonly ICurrentUser _currentUser;

            public AddGameToCollectionCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task Handle(AddGameToCollectionCommand request, CancellationToken cancellationToken)
            {
                var game = await _context.Games.FindAsync(request.gameId, cancellationToken);

                if (game == null)
                {
                    throw new EntityNotFoundException("Игра не найдена");
                }

                var collection = await _context.Collections
                    .Where(x => x.Id == request.collectionId)
                    .Include(x => x.Games)
                    .FirstOrDefaultAsync(cancellationToken);

                if (collection == null)
                {
                    throw new EntityNotFoundException("Коллекция не найдена");
                }

                var user = await _context.Users.FindAsync(_currentUser.Id, cancellationToken);

                if (collection.User != user)
                {
                    throw new UnauthorizedAccessException("Коллекция не принадлежит текущему пользователю");
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
