using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Modules.Content.Results
{
    public sealed record ContentAppError(string Message, string Code) : IError;

    public static class ContentAppErrors
    {
        // General errors
        public static ContentAppError Unauthorized => new("You are not authorized to perform this action.", ErrorCodes.Unauthorized);

        // CV errors
        public static ContentAppError OwnerNotFound => new("Couldn't find the owner.", ErrorCodes.NotFound);
        public static ContentAppError EmptyProjectCollection => new("The owner has no projects.", ErrorCodes.NotFound);

        // Post errors
        //      CRUD errors
        public static ContentAppError PostCreationFailed => new("Failed to create the post.", ErrorCodes.ActionFailed);
        public static ContentAppError PostNotFound => new("Post not found.", ErrorCodes.NotFound);
        public static ContentAppError NoPostsFound => new("No posts found.", ErrorCodes.NotFound);
        public static ContentAppError PostUpdateFailed => new("Failed to update the post.", ErrorCodes.ActionFailed);
        public static ContentAppError PostDeletionFailed => new("Failed to delete the post.", ErrorCodes.ActionFailed);
        
    }
}
