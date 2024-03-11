using BuisinessLogic.Dto.Users;
using BuisinessLogic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataAccess.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class UserController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getUserInfo/{userId:guid}")]
        public Task<GetUserInfoResponse> getUserInfo(Guid userId) {
            return _mediator.Send(new GetUserInfoQuery(userId));
        }

        [HttpGet("getUserList")]
        public Task<ICollection<GetUserListDto>> getUserList()
        {
            return _mediator.Send(new GetUserListQuery());
        }


    }
}
