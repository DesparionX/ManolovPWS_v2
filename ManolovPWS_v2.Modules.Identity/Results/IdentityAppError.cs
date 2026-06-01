using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Modules.Identity.Results
{
    public sealed record IdentityAppError(string Message, string Code) : IError;

    public static class IdentityAppErrors
    {
        // CRUD errors
        public static IdentityAppError UserCreationFailed => new("Failed to create user with the provided password.", ErrorCodes.ActionFailed);
        public static IdentityAppError UserNotFound => new("User not found.", ErrorCodes.NotFound);
        public static IdentityAppError UserUpdateFailed => new("Failed to update the user.", ErrorCodes.ActionFailed);
        public static IdentityAppError DeletionFailed => new("Failed to delete the user.", ErrorCodes.ActionFailed);
        public static IdentityAppError UserLimitReached => new("User limit reached. Cannot create more users.", ErrorCodes.Forbidden);


        // Duplication errors
        public static IdentityAppError EmailAlreadyInUse => new("The provided email is already in use.", ErrorCodes.Conflict);
        public static IdentityAppError UserNameAlreadyInUse => new("The provided username is already in use.", ErrorCodes.Conflict);

        // Auth errors
        public static IdentityAppError UserAlreadySignedIn => new("User is already signed in.", ErrorCodes.Forbidden);
        public static IdentityAppError UserNotSignedIn => new("User is not signed in.", ErrorCodes.Unauthorized);
        public static IdentityAppError UnableToAuthenticate => new("There is a problem with the authentication process.", ErrorCodes.ActionFailed);
        public static IdentityAppError InvalidCredentials => new("Invalid username or password.", ErrorCodes.Unauthorized);
    }
}
