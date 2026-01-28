using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Domain.Models.Project.Exceptions
{
    public sealed class InvalidProjectGalleryException(string message, string? code = default) 
        : Exception($"{message}, {code}")
    {
    }
}
