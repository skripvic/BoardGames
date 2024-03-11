using BuisinessLogic.Dto.Games;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuisinessLogic.Queries.Games
{
    public class GetGameListQuery : IRequest<ICollection<GetGameListDto>>
    {
        public class GetGameListQueryHandler : IRequestHandler<GetGameListQuery, ICollection<GetGameListDto>>
        {
            private readonly IApplicationDbContext _context;

            public GetGameListQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<ICollection<GetGameListDto>> Handle(GetGameListQuery request, CancellationToken cancellationToken)
            {
                var games = await _context
                    .Games
                    .Select(x => new GetGameListDto()
                    {
                        Id = x.Id,
                        Alias = x.Alias,
                        TitleEnglish = x.TitleEnglish,
                        TitleRussian = x.TitleRussian,
                        PhotoUrl = x.PhotoUrl,
                    })
                    .ToListAsync(cancellationToken);
                
                return games;
            }
        }
    }
}
