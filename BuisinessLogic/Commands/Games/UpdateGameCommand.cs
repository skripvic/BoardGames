using Azure.Core;
using BuisinessLogic.Commands.Games.Validation;
using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuisinessLogic.Commands.Games
{
    public class UpdateGameCommand : IRequest, IGameCommand
    {
        public int Id { get; init; }

        public string Alias { get; init; } = string.Empty;

        public string TitleEnglish { get; init; } = string.Empty;

        public string TitleRussian { get; init; } = string.Empty;

        public string PictureName { get; init; } = string.Empty;

        public int PlayersMin { get; init; }

        public int PlayersMax { get; init; }

        public int AgeMin { get; init; }

        public int PlayTimeMin { get; init; }

        public int PlayTimeMax { get; init; }

        public int Year { get; init; }


        public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly GameCommandValidator _validator;

            public UpdateGameCommandHandler(IApplicationDbContext context, GameCommandValidator validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task Handle(UpdateGameCommand request, CancellationToken cancellationToken)
            {

                var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (game == null)
                {
                    throw new EntityNotFoundException("Игра не найдена");
                }

                _validator.ValidateOrThrow(request);

                game.UpdateGame
                    (
                    request.Alias,
                    request.TitleEnglish,
                    request.TitleRussian,
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
