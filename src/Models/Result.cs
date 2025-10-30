namespace BugStore.Models
{
    public record Result<TError, TValue>
    {
        public TError? Error { get; }
        public TValue? Value { get; }
        public bool IsSuccess { get; }

        private Result(TError error)
        {
            Error = error;
            IsSuccess = false;
        }

        private Result(TValue value)
        {
            Value = value;
            IsSuccess = true;
        }

        public static Result<TError, TValue> Failure(TError error) => new(error);
        public static Result<TError, TValue> Success(TValue value) => new(value);
    }
}
