using ManolovPWS_v2.Shared.Abstractions.Errors;

namespace ManolovPWS_v2.Modules.Projects.Results
{
    public sealed record ProjectAppError(string Message, string Code) : IError;

    public static class ProjectAppErrors
    {
        // CRUD errors
        public static ProjectAppError ProjectCreationFailed => new("Failed to create the project.", ErrorCodes.ActionFailed);
        public static ProjectAppError ProjectNotFound => new("Project not found.", ErrorCodes.NotFound);
        public static ProjectAppError NoProjectsFound => new("No projects found.", ErrorCodes.NotFound);
        public static ProjectAppError ProjectUpdateFailed => new("Failed to update the project.", ErrorCodes.ActionFailed);
        public static ProjectAppError ProjectDeletionFailed => new("Failed to delete the project.", ErrorCodes.ActionFailed);
    }
}
