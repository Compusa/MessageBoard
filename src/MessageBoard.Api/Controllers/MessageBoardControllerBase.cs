using System;
using MessageBoard.Application.SeedWork.Results;
using StatusCodes = MessageBoard.Application.SeedWork.Results.StatusCodes;
using Microsoft.AspNetCore.Mvc;

namespace MessageBoard.Api.Controllers
{

    public abstract class MessageBoardControllerBase : Controller
    {
        protected ActionResult<T> FromResult<T>(Result<T> result)
        {
            return FromResult(result, null);
        }

        protected ActionResult<T> FromResult<T>(Result<T> result, Func<ActionResult> actionResultOnSuccess)
        {
            if (result.Succeeded)
            {
                return actionResultOnSuccess != null ? actionResultOnSuccess() : Ok(result.Value);
            }

            return FromFailedResult(result);
        }

        protected IActionResult FromResult(Result result)
        {
            if (result.Succeeded)
            {
                return Ok();
            }

            return FromFailedResult(result);
        }

        private ActionResult FromFailedResult(Result result)
        {
            return result.StatusCode switch
            {
                StatusCodes.BadRequest _ => string.IsNullOrWhiteSpace(result.Message) ? (ActionResult)BadRequest() : BadRequest(result.Message),
                StatusCodes.Forbidden _ => string.IsNullOrWhiteSpace(result.Message) ? Forbid() : Forbid(result.Message),
                StatusCodes.NotFound _ => string.IsNullOrWhiteSpace(result.Message) ? (ActionResult)NotFound() : NotFound(result.Message),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
