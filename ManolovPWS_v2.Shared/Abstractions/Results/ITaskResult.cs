using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Shared.Abstractions.Results
{
    public interface ITaskResult
    {
        bool IsSuccess { get; }
        IReadOnlyList<IError>? Errors { get; }
    }

    public interface ITaskResult<out T> : ITaskResult
    {
        T? Value { get; }
        IReadOnlyList<T>? Collection { get; }
    }
}
