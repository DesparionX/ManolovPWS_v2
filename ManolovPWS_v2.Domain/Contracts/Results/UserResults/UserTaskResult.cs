using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Results.UserResults
{
    public sealed class UserTaskResult(User? value = default, IError? error = default) : ITaskResult<User>
    {
        public User? Value { get; } = value;

        public IError? Error { get; } = error;

        public bool IsSuccess => Error is null;
    }
}
