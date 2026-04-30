using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Shared.Abstractions.Results
{
    public sealed class Result : ITaskResult
    {
        public bool IsSuccess => Errors.Count == 0;

        public IReadOnlyList<IError> Errors { get; } = [];

        private Result()
        {
        }

        private Result(IReadOnlyList<IError> errors)
        {
            Errors = errors;
        }

        public static Result Success() => new();

        public static Result Failure(IReadOnlyList<IError> errors) => new(errors);
    }

    public sealed class Result<TResponse> : ITaskResult<TResponse>
    {
        private readonly TResponse? _value;

        public bool IsSuccess => Errors.Count == 0;
        public IReadOnlyList<IError> Errors { get; } = [];

        public TResponse Value =>
            IsSuccess
                ? _value!
                : throw new InvalidOperationException("Cannot access Value when the result is not successful.");

        private Result(TResponse value)
        {
            _value = value;
        }

        private Result(IReadOnlyList<IError> errors)
        {
            Errors = errors;
        }

        public static Result<TResponse> Success(TResponse value) => new(value);

        public static Result<TResponse> Failure(IReadOnlyList<IError> errors) => new(errors);
    }
}
