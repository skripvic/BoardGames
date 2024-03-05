using BuisinessLogic.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

/*
        [HttpGet("getUserInfo/{userId:guid}")]
        public Task<GetUserInfoResponse> getUserInfo(Guid userId) {
            //return _mediator.Send(new);
        }
*/
    }
}
