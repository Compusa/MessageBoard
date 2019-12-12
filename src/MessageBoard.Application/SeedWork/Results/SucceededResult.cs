using MessageBoard.Application.SeedWork.Results.StatusCodes;

namespace MessageBoard.Application.SeedWork.Results
{
    public sealed class SucceededResult<TValue> : Result
    {
        public SucceededResult(ISucceededStatusCode statusCode, TValue value)
            : base(true, false, statusCode)
        {
            Value = value;
        }

        public TValue Value { get; }
    }
}
