using BuisinessLogic.Dto.Games;
using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;
using System.Data.Entity;

namespace BuisinessLogic.Queries.Games
{
    public class GetGameInfoQuery : IRequest<GetGameInfoResponse>
    {
        public GetGameInfoQuery(int gameId)
        {
            GameId = gameId;
        }

        private int GameId { get; }

        public class GetUserInfoQueryHandler : IRequestHandler<GetGameInfoQuery, GetGameInfoResponse>
        {
            private readonly IApplicationDbContext _context;

            public GetUserInfoQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<GetGameInfoResponse> Handle(GetGameInfoQuery request, CancellationToken cancellationToken)
            {
                var game = await _context.Games
                    .FindAsync(request.GameId, cancellationToken);

                if (game == null)
                {
                    throw new EntityNotFoundException("Игра не найдена");
                }

                return new GetGameInfoResponse
                {
                    Alias = game.Alias,
                    TitleRussian = game.TitleRussian,
                    TitleEnglish = game.TitleEnglish,
                    PlayersMax = game.PlayersMax,
                    PlayersMin = game.PlayersMin,
                    AgeMin = game.AgeMin,
                    PlayTimeMax = game.PlayTimeMax,
                    PlayTimeMin = game.PlayTimeMin,
                    Year = game.Year,
                };
            }
        }
    }
}
