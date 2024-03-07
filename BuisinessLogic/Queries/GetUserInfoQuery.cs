using BuisinessLogic.Dto;
using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuisinessLogic.Queries
{
    public class GetUserInfoQuery : IRequest<GetUserInfoResponse>
    {
        public GetUserInfoQuery(Guid userId)
        {
            UserId = userId;
        }

        private Guid UserId { get; init; }

        public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, GetUserInfoResponse>
        {
            private readonly IApplicationDbContext _systemDbContext;

            public GetUserInfoQueryHandler(IApplicationDbContext systemDbContext)
            {
                _systemDbContext = systemDbContext;
            }

            public async Task<GetUserInfoResponse> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
            {
                var user = await _systemDbContext.Users
                    .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

                if (user is null)
                {
                    throw new EntityNotFoundException("Сотрудник не найден");
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
