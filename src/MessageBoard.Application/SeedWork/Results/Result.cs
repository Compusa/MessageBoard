using MessageBoard.Application.SeedWork.Results.StatusCodes;

namespace MessageBoard.Application.SeedWork.Results
{
    public class Result
    {
        protected Result(bool succeeded, bool failed, IResultStatusCode statusCode)
        {
            Succeeded = succeeded;
            Failed = failed;
            StatusCode = statusCode;
        }

        public bool Succeeded { get; }

        public bool Failed { get; }

        public IResultStatusCode StatusCode { get; }

        public static SucceededResult<TValue> Ok<TResultStatusCode, TValue>(TValue value) where TResultStatusCode : ISucceededStatusCode, new()
        {
            return new SucceededResult<TValue>(new TResultStatusCode(), value);
        }

        public static SucceededResult<object> Ok<TResultStatusCode>() where TResultStatusCode : ISucceededStatusCode, new()
        {
            return new SucceededResult<object>(new TResultStatusCode(), null);
        }

        public static FailedResult Fail<TResultStatusCode>(string errorMessage) where TResultStatusCode : IFailedStatusCode, new()
        {
            return new FailedResult(new TResultStatusCode(), errorMessage);
        }
    }
}
