namespace ManolovPWS_v2.Shared.Authorization
{
    public static class Roles
    {
        public const string Owner = "owner";
        public const string Admin = "admin";
        public const string Moderator = "moderator";
        public const string User = "user";

        public static IReadOnlyCollection<string> AllRoles { get; } =
            [
                Owner,
                Admin,
                Moderator,
                User
            ];
    }
}
