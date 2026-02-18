using ManolovPWS_v2.Shared.Abstractions.Errors;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Modules.Identity.Results
{
    public sealed class IdentityAppResult(IReadOnlyList<IdentityAppError>? errors = default) : ITaskResult
    {
        public bool IsSuccess => Errors is null || !Errors.Any();
        public IReadOnlyList<IError>? Errors { get; } = errors;
    }

    public sealed class IdentityAppResult<TResponse>(
        TResponse? value = default,
        IReadOnlyList<TResponse>? collection = default,
        IReadOnlyList<IdentityAppError>? errors = default)
        
        : ITaskResult<TResponse>
    {
        public TResponse? Value { get; } = value;

        public IReadOnlyList<TResponse>? Collection { get; } = collection;

        public bool IsSuccess => Errors is null || !Errors.Any();

        public IReadOnlyList<IError>? Errors { get; } = errors;
    }
}
