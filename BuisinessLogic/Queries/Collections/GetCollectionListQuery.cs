using BuisinessLogic.Dto.Collections;
using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuisinessLogic.Queries.Collections
{
    public class GetCollectionListQuery : IRequest<ICollection<GetCollectionListDto>>
    {

        public GetCollectionListQuery(Guid userId)
        {
            this.userId = userId;
        }

        private Guid userId { get; init; }

        public sealed class GetCollectionListQueryHandler : IRequestHandler<GetCollectionListQuery, ICollection<GetCollectionListDto>>
        {
            private readonly IApplicationDbContext _applicationDb;

            public GetCollectionListQueryHandler(IApplicationDbContext applicationDb)
            {
                _applicationDb = applicationDb;
            }

            public async Task<ICollection<GetCollectionListDto>> Handle(GetCollectionListQuery request, CancellationToken cancellationToken)
            {
                var user = await _applicationDb.Users.FindAsync(request.userId, cancellationToken);

                if (user == null)
                {
                    throw new EntityNotFoundException("Пользователь не найден");
                }

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
