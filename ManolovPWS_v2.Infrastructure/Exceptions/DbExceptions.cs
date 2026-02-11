namespace ManolovPWS_v2.Infrastructure.Exceptions
{
    public static class DbExceptions
    {
        // User-related exceptions
        public static InfrastructureException UserNotFound(Guid userId) =>
            new($"User with ID {userId} not found", "UserNotFound");

        public static InfrastructureException UserNotFound(string email) =>
                new($"User with email {email} not found", "UserNotFound");

        public static InfrastructureException UserEmailConflict(string email) =>
                new($"A user with email {email} already exists", "UserEmailConflict");

        public static InfrastructureException UserNameConflict(string userName) =>
            new($"A user with username {userName} already exists", "UserNameConflict");

        public static InfrastructureException UserDeletionFail(string userName, IEnumerable<string?> errors) =>
            new($"Failed to delete user {userName}.\n{errors}", "UserNameConflict");

        // Project-related exceptions
        public static InfrastructureException ProjectNotFound(Guid projectId) =>
            new($"Project with ID {projectId} not found", "ProjectNotFound");

        public static InfrastructureException EmptyProjectListForUser(Guid userId) =>
            new($"No projects found for user with ID {userId}", "EmptyProjectListForUser");
    }
}
