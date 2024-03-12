using BuisinessLogic.Dto.Collections;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuisinessLogic.Queries.Collections
{
    public class GetGamesInCollectionListQuery : IRequest<ICollection<GetGamesInCollectionListDto>>
    {
        public GetGamesInCollectionListQuery(int collectionId)
        {
            this.collectionId = collectionId;
        }

        public int collectionId { get; init; }

        public sealed class GetGamesInCollectionListQueryHandler : IRequestHandler<GetGamesInCollectionListQuery, ICollection<GetGamesInCollectionListDto>>
        {
            private readonly IApplicationDbContext _applicationDb;

            public GetGamesInCollectionListQueryHandler(IApplicationDbContext applicationDb)
            {
                _applicationDb = applicationDb;
            }

            public async Task<ICollection<GetGamesInCollectionListDto>> Handle(GetGamesInCollectionListQuery request, CancellationToken cancellationToken)
            {

                var users = await _applicationDb.Collections
                    .Include(x => x.Games)
                    .Where(x => x.Id == request.collectionId)
                    .SelectMany(x => x.Games)
                    .Select(x => new GetGamesInCollectionListDto()
                    {
                        Id = x.Id,
                        Alias = x.Alias,
                        TitleEnglish = x.TitleEnglish,
                        TitleRussian = x.TitleRussian,
                        PhotoUrl = x.PhotoUrl,
                    })
                    .ToListAsync(cancellationToken);

                return users;
            }

        }
    }
}
