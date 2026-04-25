using ManolovPWS_v2.Domain.Contracts.Factories;
using ManolovPWS_v2.Domain.Contracts.Repositories;
using ManolovPWS_v2.Domain.Models.User.Properties;
using ManolovPWS_v2.Modules.Identity.Results;
using ManolovPWS_v2.Shared.Abstractions.CQRS;

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
        ) : ICommand<IdentityAppResult>;
    
    public sealed class RegisterUserCommandHandler(IUserRepository userRepository, IUserFactory userFactory)
        : ICommandHandler<RegisterUserCommand, IdentityAppResult>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserFactory _userFactory = userFactory;

        public async Task<IdentityAppResult> HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken = default)
        {
            var email = Email.Create(command.Email);
            if (await _userRepository.EmailExistsAsync(email, cancellationToken))
                return (IdentityAppResult)IdentityAppResults.Failure([new IdentityAppError("The provided email is already in use.", "EmailAlreadyInUse")]);
            
            var userName = UserName.Create(command.UserName);
            if (await _userRepository.UserNameExistsAsync(userName, cancellationToken))
                return (IdentityAppResult)IdentityAppResults.Failure([new IdentityAppError("The provided username is already in use.", "UserNameAlreadyInUse")]);
            
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

            if (result is not null)
                return (IdentityAppResult)IdentityAppResults.Success();

            return (IdentityAppResult)IdentityAppResults.Failure([new IdentityAppError("Failed to create user with the provided password.", "UserCreationFailed")]);
        }
    }
}
