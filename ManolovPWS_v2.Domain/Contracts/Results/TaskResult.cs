using ManolovPWS_v2.Domain.Abstractions;
using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Results
{
    public class TaskResult : ITaskResult
    {
        public bool IsSuccess => Error is null;

        public IError? Error {get;}

        private TaskResult(IError? error = default)
            => Error = error;

        public static ITaskResult Failure(IError error) 
            => new TaskResult(error: error);

        public static ITaskResult Success() 
            => new TaskResult();
    }
}
