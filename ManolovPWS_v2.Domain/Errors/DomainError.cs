using ManolovPWS_v2.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Errors
{
    public abstract record DomainError(string Message, string Code) : IError;
}
