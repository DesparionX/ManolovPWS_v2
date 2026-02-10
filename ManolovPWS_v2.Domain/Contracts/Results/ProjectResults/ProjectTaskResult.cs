using ManolovPWS_v2.Domain.Models.Project;
using ManolovPWS_v2.Shared.Abstractions;

namespace ManolovPWS_v2.Domain.Contracts.Results.ProjectResults
{
    public sealed class ProjectTaskResult(Project? value = default, IReadOnlyList<Project>? collection = default, IReadOnlyList<IError>? errors = default) : ITaskResult<Project>
    {
        public Project? Value { get; } = value;

        public IReadOnlyList<Project>? Collection { get; } = collection;

        public IReadOnlyList<IError>? Errors { get; } = errors;

        public bool IsSuccess => Errors is null;
    }
}
