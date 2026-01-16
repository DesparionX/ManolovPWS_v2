using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.User.Exceptions
{
    public class InvalidEmailException(string message) : Exception(message)
    {
    }
}
