using ManolovPWS_v2.Domain.Models.Project;
using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Results.ProjectResults
{
    public static class ProjectTaskResults
    {
        public static ITaskResult Success()
            => new ProjectTaskResult();

        public static ITaskResult<Project> Success(Project project)
            => new ProjectTaskResult(value: project);

        public static ITaskResult Failure(ProjectError error)
            => new ProjectTaskResult(error: error);
    }
}
