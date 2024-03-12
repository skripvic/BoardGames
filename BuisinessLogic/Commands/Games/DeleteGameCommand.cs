using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;

namespace BuisinessLogic.Commands.Games
{
    public class DeleteGameCommand : IRequest
    {
        public int Id { get; init; }

        public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteGameCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(DeleteGameCommand request, CancellationToken cancellationToken)
            {

                var game = await _context.Games
                    .FindAsync(request.Id, cancellationToken);

                if (game == null)
                {
                    throw new EntityNotFoundException("Игра не найдена");
                }

                _context.Games.Remove(game);

                await _context.SaveChangesAsync(cancellationToken);

            }
        }
    }
}
