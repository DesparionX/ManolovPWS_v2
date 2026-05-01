using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.DeleteUser
{
    public sealed record DeleteUserCommand(string UserId) : ICommand<ITaskResult>;

    public sealed class DeleteUserCommandHandler(IUserRepository userRepository) 
        : ICommandHandler<DeleteUserCommand, ITaskResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<ITaskResult> HandleAsync(DeleteUserCommand command, CancellationToken cancellationToken = default)
        {
            var userId = UserId.From(command.UserId);

            var result = await _userRepository.RemoveAsync(userId,  cancellationToken);

            return result.IsSuccess ?
                Result.Success()
                : Result.Failure(result.Errors);
        }
    }
}
