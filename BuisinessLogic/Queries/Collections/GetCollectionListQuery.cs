using BuisinessLogic.Auth.CurrentUser;
using BuisinessLogic.Dto.Collections;
using BuisinessLogic.Exceptions;
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
            private readonly ICurrentUser _currentUser;

            public GetCollectionListQueryHandler(IApplicationDbContext applicationDb, ICurrentUser currentUser)
            {
                _applicationDb = applicationDb;
                _currentUser = currentUser;
            }

            public async Task<ICollection<GetCollectionListDto>> Handle(GetCollectionListQuery request, CancellationToken cancellationToken)
            {
                var user = await _applicationDb.Users.FindAsync(_currentUser.Id, cancellationToken);

                var collections = await _applicationDb
                    .Collections
                    .Where(c => c.User == user)
                    .Select(u => new GetCollectionListDto()
                    {
                        Id = u.Id,
                        Name = u.Name,
                    })
                    .ToListAsync(cancellationToken);


                return collections;
            }

        }
    }

}
