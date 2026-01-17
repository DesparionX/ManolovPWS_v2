using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User.Exceptions
{
    public sealed class InvalidGenderException(string message): Exception(message)
    {
    }
}
