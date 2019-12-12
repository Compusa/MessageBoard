using System;
using MessageBoard.Application.SeedWork.Results;
using StatusCodes = MessageBoard.Application.SeedWork.Results.StatusCodes;
using Microsoft.AspNetCore.Mvc;
namespace MessageBoard.Api.Controllers
{

    public abstract class MessageBoardControllerBase : Controller
    {
        protected IActionResult FromResult(Result result)
        {
            if (result.Succeeded)
            {
                var succeedResult = (SucceededResult<object>)result;

                return succeedResult.Value != null ? (ActionResult)Ok(succeedResult.Value) : Ok();
            }

            var failedResult = (FailedResult)result;

            return failedResult.StatusCode switch
            {
                StatusCodes.BadRequest _ => string.IsNullOrWhiteSpace(failedResult.Message) ? (ActionResult)BadRequest() : BadRequest(failedResult.Message),
                StatusCodes.Forbidden _ => string.IsNullOrWhiteSpace(failedResult.Message) ? Forbid() : Forbid(failedResult.Message),
                StatusCodes.NotFound _ => string.IsNullOrWhiteSpace(failedResult.Message) ? (ActionResult)NotFound() : NotFound(failedResult.Message),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
