namespace ManolovPWS_v2.Shared.Abstractions
{
    public interface ITaskResult
    {
        bool IsSuccess { get; }
        IError? Error { get; }
    }

    public interface ITaskResult<out T> : ITaskResult
    {
        T? Value { get; }
    }
}
