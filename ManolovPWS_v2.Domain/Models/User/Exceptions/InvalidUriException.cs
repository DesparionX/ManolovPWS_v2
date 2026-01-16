using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User.Exceptions
{
    public class InvalidUriException(string message) : Exception(message)
    {
    }
}
