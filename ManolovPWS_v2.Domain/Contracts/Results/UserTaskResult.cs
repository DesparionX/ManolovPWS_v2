using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Results
{
    public class UserTaskResult : ITaskResult<User>
    {
        public User? Value { get; }

        public bool IsSuccess => Error is null;

        public IError? Error { get; }

        private UserTaskResult(User? value = default, IError? error = default)
        {
            Value = value;
            Error = error;
        }

        public static ITaskResult Failure(IError error)
            => new UserTaskResult(error: error);

        public static ITaskResult Success(User value)
            => new UserTaskResult(value: value);

        public static ITaskResult Success()
            => new UserTaskResult();
    }
}
