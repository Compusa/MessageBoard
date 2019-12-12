using MessageBoard.Application.SeedWork.Results.StatusCodes;

namespace MessageBoard.Application.SeedWork.Results
{
    public class FailedResult : Result
    {
        public FailedResult(IFailedStatusCode statusCode, string errorMessage)
            : base(false, true, statusCode)
        {
            Message = errorMessage;
        }

        public string Message { get; }
    }
}
