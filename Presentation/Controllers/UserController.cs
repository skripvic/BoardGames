using BuisinessLogic.Auth.Base;
using BuisinessLogic.Commands.Auth;
using BuisinessLogic.Commands.Users;
using BuisinessLogic.Dto.Users;
using BuisinessLogic.Queries;
using DomainLayer.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("getUserInfo")]
        [Authorize]
        public Task<GetUserInfoResponse> getUserInfo() {
            return _mediator.Send(new GetUserInfoQuery(), HttpContext.RequestAborted);
        }

        [HttpGet("getUserList")]
        [Authorize]
        public Task<ICollection<GetUserListDto>> getUserList()
        {
            return _mediator.Send(new GetUserListQuery(), HttpContext.RequestAborted);
        }

        [HttpPost("registration")]
        public Task<AuthResponse> registration(RegistrationCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

        [HttpPost("signIn")]
        public Task<AuthResponse> signIn(SignInCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

        [HttpPost("validateJwt")]
        [Authorize]
        public Task<ValidateJwtResponse> validateJwt(ValidateJwtCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

        [HttpPut("updateUser")]
        [Authorize]
        public Task updateUser(UpdateUserCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

        [HttpDelete("deleteUser")]
        [Authorize]
        public Task deleteUser(DeleteUserCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

    }
}
