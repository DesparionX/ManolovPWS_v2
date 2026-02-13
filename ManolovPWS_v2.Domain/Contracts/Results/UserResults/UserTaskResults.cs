using ManolovPWS_v2.Domain.Models.User;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Domain.Contracts.Results.UserResults
{
    public static class UserTaskResults
    {
        public static ITaskResult Success()
            => new UserTaskResult();

        public static ITaskResult<User> Success(User user)
            => new UserTaskResult(value: user);

        public static ITaskResult<User> Success(IReadOnlyList<User> users)
            => new UserTaskResult(collection: users);

        public static ITaskResult Failure(IReadOnlyList<UserError> errors)
            => new UserTaskResult(errors: errors);
    }
}
