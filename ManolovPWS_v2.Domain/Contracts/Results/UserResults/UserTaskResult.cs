using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Results.UserResults
{
    public sealed class UserTaskResult(User? value = default, IReadOnlyList<User>? collection = default, IReadOnlyList<IError>? errors = default) : ITaskResult<User>
    {
        public User? Value { get; } = value;

        public IReadOnlyList<User>? Collection { get; } = collection;

        public IReadOnlyList<IError>? Errors { get; } = errors;

        public bool IsSuccess => Errors is null;
    }
}
