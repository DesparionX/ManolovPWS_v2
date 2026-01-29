using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.Post.Exceptions
{
    public sealed class InvalidPostPublishedDateException(string message) : Exception(message)
    {
    }
}
