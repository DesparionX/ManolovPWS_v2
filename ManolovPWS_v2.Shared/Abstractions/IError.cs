using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ManolovPWS_v2.Shared.Abstractions
{
    public interface IError
    {
        string Message { get; }
        string Code { get; }
    }
}
