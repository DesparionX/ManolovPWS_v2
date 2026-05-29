using ManolovPWS_v2.Domain.Contracts.Factories;
using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.User.Features.RegisterUser
{
    public sealed record RegisterUserCommand(
        string UserName,
        string Email,
        string Password,
        string FirstName,
        string? MiddleName,
        string Profession,
        string LastName,
        string Gender,
        DateOnly BirthDate
        ) : ICommand;
    
    public sealed class RegisterUserCommandHandler(IUserRepository userRepository, IUserFactory userFactory)
        : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserFactory _userFactory = userFactory;

        public async Task<ITaskResult> HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken = default)
        {
            // For the application purpose, there can be only one user (the app owner).
            // This is a business rule that can be changed in the future if needed.
            if(await _userRepository.AnyAsync(cancellationToken))
                return Result.Failure([IdentityAppErrors.UserLimitReached]);


            var email = Email.Create(command.Email);
            if (await _userRepository.EmailExistsAsync(email, cancellationToken))
                return Result.Failure([IdentityAppErrors.EmailAlreadyInUse]);
            
            var userName = UserName.Create(command.UserName);
            if (await _userRepository.UserNameExistsAsync(userName, cancellationToken))
                return Result.Failure([IdentityAppErrors.UserNameAlreadyInUse]);

            var id = UserId.New();
            var name = Name.Create(firstName: command.FirstName, lastName: command.LastName, middleName: command.MiddleName);
            var profession = Profession.Create(command.Profession);
            var birthDate = BirthDate.Create(command.BirthDate);
            var gender = Gender.FromString(command.Gender);

            var user = Domain.Models.User.User.Create(
                id: id,
                userName: userName,
                email: email,
                name: name,
                profession: profession,
                birthDate: birthDate,
                gender: gender
                );

            var result = await _userFactory.CreateWithPasswordAsync(user, command.Password, cancellationToken);

            if (!result.IsSuccess || result.Value is null)
                return Result.Failure([IdentityAppErrors.UserCreationFailed, ..result.Errors]);

            return Result.Success();
        }
    }
}
