using BuisinessLogic.Auth.CurrentUser;
using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuisinessLogic.Commands.Collections
{
    public class DeleteGameFromCollectionCommand : IRequest
    {
        public int collectionId { get; init; }

        public int gameId { get; init; }

        public class DeleteGameFromCollectionCommandHandler : IRequestHandler<DeleteGameFromCollectionCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUser _currentUser;

            public DeleteGameFromCollectionCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task Handle(DeleteGameFromCollectionCommand request, CancellationToken cancellationToken)
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

                var user = await _context.Users.FindAsync(_currentUser.Id, cancellationToken);

                if (collection.User != user)
                {
                    throw new UnauthorizedAccessException("Коллекция не принадлежит текущему пользователю");
                }

                if (collection.Games.All(x => x.Id != game.Id))
                {
                    throw new BadRequestException("Данной игры нет в коллекции");
                }

                collection.Games.Remove(game);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
