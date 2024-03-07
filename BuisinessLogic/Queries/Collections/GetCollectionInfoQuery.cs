using BuisinessLogic.Dto.Collections;
using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuisinessLogic.Queries.Collections
{
    public class GetCollectionInfoQuery : IRequest<GetCollectionInfoResponse>
    {
        public GetCollectionInfoQuery(int collectionId)
        {
            CollectionId = collectionId;
        }

        private int CollectionId { get; init; }

        public class GetCollectionInfoQueryHandler : IRequestHandler<GetCollectionInfoQuery, GetCollectionInfoResponse>
        {
            private readonly IApplicationDbContext _applicationDb;

            public GetCollectionInfoQueryHandler(IApplicationDbContext applicationDb)
            {
                _applicationDb = applicationDb;
            }

            public async Task<GetCollectionInfoResponse> Handle(GetCollectionInfoQuery request, CancellationToken cancellationToken)
            {
                var collection = await _applicationDb.Collections
                    .FirstOrDefaultAsync(x => x.Id == request.CollectionId, cancellationToken);

                if (collection is null)
                {
                    throw new EntityNotFoundException("Коллекция не найдена");
                }

                return new GetCollectionInfoResponse
                {
                    Name = collection.Name
                };
            }
        }
    }
}
