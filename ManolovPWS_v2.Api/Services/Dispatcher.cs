using ManolovPWS_v2.Shared.Abstractions.CQRS;
using ManolovPWS_v2.Shared.Abstractions.Results;

namespace ManolovPWS_v2.Api.Services
{
    public sealed class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task<ITaskResult<TResponse>> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(ICommandHandler<,>)
                .MakeGenericType(command.GetType(), typeof(TResponse));

            dynamic handler = _serviceProvider.GetRequiredService(handlerType);

            return await handler.HandleAsync((dynamic)command, cancellationToken);
        }

        public async Task<ITaskResult> SendAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(ICommandHandler<,>)
                .MakeGenericType(command.GetType(), typeof(object));

            dynamic handler = _serviceProvider.GetRequiredService(handlerType);

            return await handler.HandleAsync(command, cancellationToken);
        }

        public async Task<ITaskResult<TResponse>> QueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResponse));

            dynamic handler = _serviceProvider.GetRequiredService(handlerType);

            return await handler.HandleAsync((dynamic)query, cancellationToken);
        }
    }
}
