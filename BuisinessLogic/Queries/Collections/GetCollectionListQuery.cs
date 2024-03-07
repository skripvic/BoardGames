using BuisinessLogic.Dto.Collections;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuisinessLogic.Queries.Collections
{
    public class GetCollectionListQuery : IRequest<ICollection<GetCollectionListDto>>
    {
    public sealed class GetCollectionListQueryHandler : IRequestHandler<GetCollectionListQuery, ICollection<GetCollectionListDto>>
        {
            private readonly IApplicationDbContext _applicationDb;

            public GetCollectionListQueryHandler(IApplicationDbContext applicationDb)
            {
                _applicationDb = applicationDb;
            }

            public async Task<ICollection<GetCollectionListDto>> Handle(GetCollectionListQuery request, CancellationToken cancellationToken)
            {

                var users = await _applicationDb
                    .Users
                    .Select(u => new GetCollectionListDto()
                    {
                        Name = u.Name,
                    })
                    .ToListAsync(cancellationToken);

                return users;
            }

        }
    }

}
}
