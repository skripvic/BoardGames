using BuisinessLogic.Dto.Collections;
using BuisinessLogic.Dto.Users;
using BuisinessLogic.Queries;
using BuisinessLogic.Queries.Collections;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController
    {
        private readonly IMediator _mediator;

        public CollectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getCollectionInfo/{collectionId:int}")]
        public Task<GetCollectionInfoResponse> getCollectionInfo(int collectionId)
        {
            return _mediator.Send(new GetCollectionInfoQuery(collectionId));
        }

        [HttpGet("getCollectionList")]
        public Task<ICollection<GetCollectionListDto>> getCollectionList()
        {
            return _mediator.Send(new GetCollectionListQuery());
        }


    }
}
