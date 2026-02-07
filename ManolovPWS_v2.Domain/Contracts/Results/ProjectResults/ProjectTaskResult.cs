using ManolovPWS_v2.Domain.Models.Project;
using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Results.ProjectResults
{
    public sealed class ProjectTaskResult(Project? value = default, IError? error = default) : ITaskResult<Project>
    {
        public Project? Value { get; } = value;

        public IError? Error { get; } = error;

        public bool IsSuccess => Error is null;
    }
}
