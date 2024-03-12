using BuisinessLogic.Commands.Users;
using BuisinessLogic.Dto.Users;
using BuisinessLogic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataAccess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getUserInfo/{userId:guid}")]
        public Task<GetUserInfoResponse> getUserInfo(Guid userId) {
            return _mediator.Send(new GetUserInfoQuery(userId), HttpContext.RequestAborted);
        }

        [HttpGet("getUserList")]
        public Task<ICollection<GetUserListDto>> getUserList()
        {
            return _mediator.Send(new GetUserListQuery(), HttpContext.RequestAborted);
        }

        [HttpPost("createUser")]
        public Task<CreateUserCommandResponse> createUser(CreateUserCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

        [HttpPut("updateUser")]
        public Task updateUser(UpdateUserCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

        [HttpDelete("deleteUser")]
        public Task deleteUser(DeleteUserCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

    }
}
