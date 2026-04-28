using System;
using System.Collections.Generic;
using System.Text;

namespace ManolovPWS_v2.Infrastructure.Contracts.Auth.JWT
{
    public sealed class JwtSettings
    {
        public string Key { get; init; } = default!;
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;
        public int ExpiryMinutes { get; init; }
    }
}
