using BuisinessLogic.Commands.Collections;
using BuisinessLogic.Dto.Collections;
using BuisinessLogic.Queries.Collections;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CollectionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CollectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getCollectionInfo/{collectionId:int}")]
        public Task<GetCollectionInfoResponse> getCollectionInfo(int collectionId)
        {
            return _mediator.Send(new GetCollectionInfoQuery(collectionId), HttpContext.RequestAborted);
        }

        [HttpGet("getCollectionList")]
        public Task<ICollection<GetCollectionListDto>> getCollectionList()
        {
            return _mediator.Send(new GetCollectionListQuery(), HttpContext.RequestAborted);
        }

        [HttpGet("getGamesInCollectionList/{collectionId:int}")]
        public Task<ICollection<GetGamesInCollectionListDto>> getGamesInCollectionList(int collectionId)
        {
            return _mediator.Send(new GetGamesInCollectionListQuery(collectionId), HttpContext.RequestAborted);
        }

        [HttpPost("createCollection")]
        public Task<CreateCollectionCommandResponse> createCollection(CreateCollectionCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

        [HttpPost("addGameToCollection")]
        public Task addGameToCollection(AddGameToCollectionCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

        [HttpPut("updateCollection")]
        public Task updateCollection(UpdateCollectionCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

        [HttpDelete("deleteCollection")]
        public Task deleteCollection(DeleteCollectionCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted);
        }

        [HttpDelete("deleteGameFromCollection")]
        public Task deleteGameFromCollection(DeleteGameFromCollectionCommand command)
        {
            return _mediator.Send(command, HttpContext.RequestAborted   );
        }


    }
}
