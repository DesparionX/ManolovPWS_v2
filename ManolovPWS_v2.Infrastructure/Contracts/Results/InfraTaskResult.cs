using ManolovPWS_v2.Shared.Abstractions.Errors;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Infrastructure.Contracts.Results
{
    public sealed class InfraTaskResult(IReadOnlyList<IError>? errors = default) : ITaskResult
    {
        public bool IsSuccess => Errors is null || !Errors.Any();

        public IReadOnlyList<IError>? Errors { get; } = errors;
    }
}
