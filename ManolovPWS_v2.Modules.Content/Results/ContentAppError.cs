using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Modules.Content.Results
{
    public sealed record ContentAppError(string Message, string Code) : IError;

    public static class ContentAppErrors
    {
        public static ContentAppError OwnerNotFound => new("Couldn't find the owner.", ErrorCodes.NotFound);
        public static ContentAppError EmptyProjectCollection => new("The owner has no projects.", ErrorCodes.NotFound);
    }
}
