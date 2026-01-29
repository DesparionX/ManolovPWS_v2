using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.Post.Exceptions
{
    public sealed class InvalidPostContentException(string message, string code)
        : Exception($"{message}, {code}")
    {
    }
}
