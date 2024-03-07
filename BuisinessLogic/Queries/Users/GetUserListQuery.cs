using BuisinessLogic.Dto.Users;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuisinessLogic.Queries
{
    public class GetUserListQuery : IRequest<ICollection<GetUserListDto>>
    {
    public sealed class GetUsersListQueryHandler : IRequestHandler<GetUserListQuery, ICollection<GetUserListDto>>
        {
            private readonly IApplicationDbContext _applicationDb;

            public GetUsersListQueryHandler(IApplicationDbContext applicationDb)
            {
                _applicationDb = applicationDb;
            }

            public async Task<ICollection<GetUserListDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
            {

                var users = await _applicationDb
                    .Users
                    .Select(u => new GetUserListDto()
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email,
                    })
                    .ToListAsync(cancellationToken);

                return users;
            }

        }
    }
}
