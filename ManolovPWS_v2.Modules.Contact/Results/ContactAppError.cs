using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Modules.Contact.Results
{
    public sealed record ContactAppError(string Message, string Code) : IError;

    public static class ContactAppErrors
    {
        // General errors
        public static ContactAppError Unauthorized => new("You are not authorized to perform this action.", ErrorCodes.Unauthorized);

        // Contact errors
        public static ContactAppError FailedToSendMessage => new("Failed to send the message.", ErrorCodes.ActionFailed);
        public static ContactAppError CannotSpam(int mins) 
            => new($"You can only send one message every {mins} minutes. Please wait before sending another message.", ErrorCodes.ActionFailed);
        public static ContactAppError MessageNotFound => new("Message not found.", ErrorCodes.NotFound);
        public static ContactAppError NoMessagesFound => new("No messages found.", ErrorCodes.NotFound);
        public static ContactAppError MessageDeletionFailed => new("Failed to delete the message.", ErrorCodes.ActionFailed);
        public static ContactAppError FailedToReadMessage => new("Failed to read the message.", ErrorCodes.ActionFailed);
    }
}
