using BuisinessLogic.Auth.CurrentUser;
using BuisinessLogic.Dto.Users;
using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;

namespace BuisinessLogic.Queries
{
    public class GetUserInfoQuery : IRequest<GetUserInfoResponse>
    {

        public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, GetUserInfoResponse>
        {
            private readonly IApplicationDbContext _applicationDb;
            private readonly ICurrentUser _currentUser;

            public GetUserInfoQueryHandler(IApplicationDbContext applicationDb, ICurrentUser currentUser)
            {
                _applicationDb = applicationDb;
                _currentUser = currentUser;
            }

            public async Task<GetUserInfoResponse> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
            {
                var user = await _applicationDb.Users
                    .FindAsync(_currentUser.Id, cancellationToken);

                if (user is null)
                {
                    throw new EntityNotFoundException("Пользователь не найден");
                }

                return new GetUserInfoResponse
                {
                    Name = user.Name,
                    Email = user.Email,

                };
            }
        }


    }
}
