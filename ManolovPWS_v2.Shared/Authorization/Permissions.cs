using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Shared.Authorization
{
    public static class Permissions
    {
        // Admin Permissions
        public const string ManageUsers = "users.manage";
        public const string ManageRoles = "roles.manage";
        public const string ManageMessages = "messages.manage";

        // Post related permissions
        public const string CreatePost = "posts.create";
        public const string EditPost = "posts.edit";
        public const string DeletePost = "posts.delete";

        // Project related permissions
        public const string AddProject = "projects.add";
        public const string EditProject = "projects.edit";
        public const string DeleteProject = "projects.delete";

        public static IReadOnlyCollection<string> AllPermissions { get; } =
        [
            ManageUsers,
            ManageRoles,
            ManageMessages,
            CreatePost,
            EditPost,
            DeletePost,
            AddProject,
            EditProject,
            DeleteProject
        ];
    }
}
