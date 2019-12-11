using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MessageBoard.Application;
using MessageBoard.Application.Messages.Commands;
using MessageBoard.Application.Messages.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessageBoard.Api.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Find message by id.
        /// </summary>
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

        /// <summary>
        /// List all messages.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> Get()
        {
            var messages = await _mediator.Send(new ListMessagesQuery());

            return Ok(messages);
        }

        /// <summary>
        /// Creates a new message.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MessageDto>> Post([FromBody] string message)
        {
            var createdMessage = await _mediator.Send(new CreateMessageCommand(message, 1));

            return CreatedAtAction(nameof(Get), new { id = createdMessage.Id }, createdMessage);
        }

        /// <summary>
        /// Updates a message.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] string message)
        {
            var updatedMessage = await _mediator.Send(new UpdateMessageCommand(id, message, 1));

            if (updatedMessage == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Deletes a message.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var messageDeleted = await _mediator.Send(new DeleteMessageCommand(id, 1));

            if (!messageDeleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
