using BuisinessLogic.Commands.Games;
using BuisinessLogic.Dto.Games;
using BuisinessLogic.Queries.Games;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getGameInfo/{gameId:int}")]
        public Task<GetGameInfoResponse> getGameInfo(int gameId)
        {
            return _mediator.Send(new GetGameInfoQuery(gameId), HttpContext.RequestAborted);
        }

        [HttpGet("getGameList")]
        public Task<ICollection<GetGameListDto>> getGameList()
        {
            return _mediator.Send(new GetGameListQuery(), HttpContext.RequestAborted    );
        }

        [HttpPost("createGame")]
        public Task<CreateGameCommandResponse> createGame(CreateGameCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

        [HttpPut("updateGame")]
        public Task updateGame(UpdateGameCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

        [HttpDelete("deleteGame")]
        public Task deleteGame(DeleteGameCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }



    }
}
