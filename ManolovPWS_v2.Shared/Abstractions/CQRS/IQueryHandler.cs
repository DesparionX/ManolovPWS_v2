using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Shared.Abstractions.CQRS
{
    public interface IQueryHandler<in TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task<ITaskResult<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
