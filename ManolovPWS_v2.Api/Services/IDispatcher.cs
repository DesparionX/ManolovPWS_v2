using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Api.Services
{
    public interface IDispatcher
    {
        Task<ITaskResult<TResponse>> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
        Task<ITaskResult> SendAsync(ICommand command, CancellationToken cancellationToken = default);

        Task<ITaskResult<TResponse>> QueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
    }
}
