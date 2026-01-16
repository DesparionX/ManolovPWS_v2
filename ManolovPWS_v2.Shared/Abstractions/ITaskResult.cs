using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Shared.Abstractions
{
    public interface ITaskResult
    {
        bool IsSuccess { get; }
        IError? Error { get; }

        static abstract ITaskResult Success();
        static abstract ITaskResult Failure(IError error);
    }

    public interface ITaskResult<T> : ITaskResult
    {
        T? Value { get; }
        static abstract ITaskResult<T> Success(T value);
        static new abstract ITaskResult<T> Failure(IError error);
    }
}
