using BuisinessLogic.Dto.Games;
using DataAccess;
using DomainLayer.Entities;
using MediatR;

namespace BuisinessLogic.Commands.Games
{
    public class CreateGameCommand : IRequest<CreateGameCommandResponse>
    {
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


        public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, CreateGameCommandResponse>
        {
            private readonly IApplicationDbContext _context;

            public CreateGameCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<CreateGameCommandResponse> Handle(CreateGameCommand request, CancellationToken cancellationToken)
            {
                var newGame = new Game
                    (
                        request.Alias,
                        request.TitleRussian,
                        request.TitleEnglish,
                        request.PhotoUrl,
                        request.PlayersMin,
                        request.PlayersMax,
                        request.AgeMin,
                        request.PlayTimeMin,
                        request.PlayTimeMax,
                        request.Year
                    );

                _context.Games.Add( newGame );

                await _context.SaveChangesAsync( cancellationToken );

                return new CreateGameCommandResponse()
                {
                    Id = newGame.Id
                };
            }
        }

    }
}
