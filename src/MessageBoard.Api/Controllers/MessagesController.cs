using System;
using System.Threading.Tasks;
using MediatR;
using MessageBoard.Application;
using MessageBoard.Application.Messages.Queries;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using MessageBoard.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessageBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator, MessageBoardContext messageBoardContext)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            
            // TODO: Dummy data, to be removed...
            messageBoardContext.Messages.Add(new Message { Id = 3 });
            messageBoardContext.Messages.Add(new Message { Id = 4 });
            messageBoardContext.Messages.Add(new Message { Id = 2 });
            messageBoardContext.Messages.Add(new Message { Id = 1 });
            messageBoardContext.SaveChanges();
        }

        /// <summary>
        /// Find message by id.
        /// </summary>
        /// <response code="200">The message with the specified id.</response>
        /// <response code="404">If message with specified id doesn't exist</response>
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MessageDto>> Get(int id)
        {
            var message = await _mediator.Send(new GetMessageQuery(id));

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }
    }
}
