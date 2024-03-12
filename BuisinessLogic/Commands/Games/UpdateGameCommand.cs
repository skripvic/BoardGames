using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuisinessLogic.Commands.Games
{
    public class UpdateGameCommand : IRequest
    {
        public int Id { get; init; }

        public string Alias { get; init; } = string.Empty;

        public string TitleEnglish { get; init; } = string.Empty;

        public string TitleRussian { get; init; } = string.Empty;

        public string PhotoUrl { get; init; } = string.Empty;

        public int PlayersMin { get; init; }

        public int PlayersMax { get; init; }

        public int AgeMin { get; init; }

        public int PlayTimeMin { get; init; }

        public int PlayTimeMax { get; init; }

        public int Year { get; init; }


        public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateGameCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(UpdateGameCommand request, CancellationToken cancellationToken)
            {

                var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (game == null)
                {
                    throw new EntityNotFoundException("Игра не найдена");
                }

                game.UpdateGame
                    (
                    request.Alias,
                    request.TitleEnglish,
                    request.TitleRussian,
                    request.PhotoUrl,
                    request.PlayersMin,
                    request.PlayersMax,
                    request.AgeMin,
                    request.PlayTimeMin,
                    request.PlayTimeMax,
                    request.Year
                    );
                
                await _context.SaveChangesAsync(cancellationToken);

            }
        }
    }
}
