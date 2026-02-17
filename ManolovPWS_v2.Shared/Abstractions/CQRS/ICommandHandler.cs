using ManolovPWS_v2.Shared.Abstractions.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Shared.Abstractions.CQRS
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task<ITaskResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }

    public interface ICommandHandler<in TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        Task<ITaskResult<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
