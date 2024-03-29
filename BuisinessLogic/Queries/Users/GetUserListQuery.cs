using BuisinessLogic.Auth;
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
            private readonly IAuthService _tokenService;

            public GetUsersListQueryHandler(IApplicationDbContext applicationDb, IAuthService tokenService)
            {
                _applicationDb = applicationDb;
                _tokenService = tokenService;
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
