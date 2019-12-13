using MessageBoard.Application.SeedWork.Results.StatusCodes;

namespace MessageBoard.Application.SeedWork.Results
{
    public class Result
    {
        protected Result(bool succeeded, bool failed, string message, IResultStatusCode statusCode)
        {
            Succeeded = succeeded;
            Failed = failed;
            Message = message;
            StatusCode = statusCode;
        }

        public bool Succeeded { get; }

        public bool Failed { get; }

        public IResultStatusCode StatusCode { get; }

        public string Message { get; }

        public static Result<TValue> Ok<TResultStatusCode, TValue>(TValue value) where TResultStatusCode : ISucceededStatusCode, new()
        {
            return new Result<TValue>(true, false, null, new TResultStatusCode(), value);
        }

        public static Result Ok<TResultStatusCode>() where TResultStatusCode : ISucceededStatusCode, new()
        {
            return new Result(true, false, null, new TResultStatusCode());
        }

        public static Result<TValue> Fail<TResultStatusCode, TValue>(string message) where TResultStatusCode : IFailedStatusCode, new()
        {
            return new Result<TValue>(false, true, message, new TResultStatusCode(), default(TValue));
        }

        public static Result Fail<TResultStatusCode>(string message) where TResultStatusCode : IFailedStatusCode, new()
        {
            return new Result(false, true, message, new TResultStatusCode());
        }

        public static Result Fail<TResultStatusCode>() where TResultStatusCode : IFailedStatusCode, new()
        {
            return new Result(false, true, null, new TResultStatusCode());
        }
    }

    public sealed class Result<TValue> : Result
    {
        public Result(bool succeeded, bool failed, string message, IResultStatusCode statusCode, TValue value)
            : base(succeeded, failed, message, statusCode)
        {
            Value = value;
        }

        public TValue Value { get; }
    }
}
